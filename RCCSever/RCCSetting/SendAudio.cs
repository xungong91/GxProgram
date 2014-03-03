using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectSound;
using System.IO;
using Microsoft.DirectX;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Windows;

namespace RCCSever.RCCSetting
{
    public class SendAudio:SendBase
    {
        private string strRecSaveFile = string.Empty;//文件保存路径
        private Notify myNotify = null;//缓冲区提示事件
        private MemoryStream fsWav = null;//保存的文件流
        private int iNotifyNum = 16;//通知的个数
        /// <summary>
        /// 本次数据起始点， 上一次数据的终点。
        /// </summary>
        private int iBufferOffset = 0;
        /// <summary>
        /// 所采集到的数据大小
        /// </summary>
        private int iSampleSize = 0;
        private int iNotifySize = 0;//通知所在区域大小
        private int iBufferSize = 0;//缓冲区大小
        private BinaryWriter mWriter;
        private Microsoft.DirectX.DirectSound.Capture capture = null;//捕捉设备对象
        /// <summary>
        /// 捕捉缓冲区
        /// </summary>
        private CaptureBuffer capturebuffer = null;
        private AutoResetEvent notifyevent = null;
        private Thread notifythread = null;
        private WaveFormat mWavFormat;//PCM格式

        public SendAudio(IPEndPoint ipPoint, int YesNum, int NoNum)
            : base(ipPoint, YesNum, NoNum)
        {
            this.ipPoint = ipPoint;
            this.YesNum = YesNum;
            this.NoNum = NoNum;
            //设置pcm
            mWavFormat = SetWaveFormat();
            //设置驱动
            CreateCaputerDevice();
        }
        private bool IsFast = true;
        MemoryStream memorystreamData = new MemoryStream();
        private object lockThis = new object();
        private void Send(object obj)
        {
            byte[] capturedata = obj as byte[];
            byte[] bytedata = null;
            lock (lockThis)
            {
                memorystreamData.Write(capturedata, 0, capturedata.Length);
                if (memorystreamData.Length < 1024 * 8) return;
                bytedata = memorystreamData.ToArray();
                memorystreamData = new MemoryStream();
            }
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            CreateWaveFile(bw);
            bw.Write(bytedata, 0, bytedata.Length);//写入到文件
            //写WAV文件尾
            bw.Seek(4, SeekOrigin.Begin);
            bw.Write((int)(bytedata.Length + 36));   // 写文件长度
            bw.Seek(40, SeekOrigin.Begin);
            bw.Write(bytedata.Length);                // 写数据长度
            bw.Close();
            bw.Dispose();

            MemoryStream mememorys=new MemoryStream(ms.ToArray());
            base.BeginSend(mememorys);
        }
        public static MemoryStream Receive(MemoryStream ms)
        {
            //转码
            MemoryStream memorys=new MemoryStream(ms.ToArray());
            return memorys;
        }

        public void Start()
        {
            //设置文件头
            //CreateWaveFile();
            //设置缓冲区 初始化捕捉缓冲区
            CreateCaptureBuffer();
            //设置通知
            CreateNotification();
            capturebuffer.Start(true);
        }

        public static WaveFormat SetWaveFormat()
        {
            WaveFormat format = new WaveFormat();
            format.FormatTag = WaveFormatTag.Pcm;//设置音频类型
            //format.SamplesPerSecond = 22050;//采样率（单位：赫兹）典型值：11025、22050、44100Hz
            format.SamplesPerSecond = 44100;
            format.BitsPerSample = 16;//采样位数
            format.Channels = 1;//声道
            format.BlockAlign = (short)(format.Channels * (format.BitsPerSample / 8));//单位采样点的字节数
            format.AverageBytesPerSecond = format.BlockAlign * format.SamplesPerSecond;
            return format;
            //按照以上采样规格，可知采样1秒钟的字节数为22050*2=55100B 约为 53K
        }
        private void CreateWaveFile(BinaryWriter bw)
        {
            char[] ChunkRiff = { 'R', 'I', 'F', 'F' };
            char[] ChunkType = { 'W', 'A', 'V', 'E' };
            char[] ChunkFmt = { 'f', 'm', 't', ' ' };
            char[] ChunkData = { 'd', 'a', 't', 'a' };
            short shPad = 1;                // File padding
            int nFormatChunkLength = 0x10;  // Format chunk length.
            int nLength = 0;                // File length, minus first 8 bytes of RIFF description. This will be filled in later.
            short shBytesPerSample = 0;     // Bytes per sample.
            // 一个样本点的字节数目
            if (8 == mWavFormat.BitsPerSample && 1 == mWavFormat.Channels)
                shBytesPerSample = 1;
            else if ((8 == mWavFormat.BitsPerSample && 2 == mWavFormat.Channels) || (16 == mWavFormat.BitsPerSample && 1 == mWavFormat.Channels))
                shBytesPerSample = 2;
            else if (16 == mWavFormat.BitsPerSample && 2 == mWavFormat.Channels)
                shBytesPerSample = 4;
            // RIFF 块
            bw.Write(ChunkRiff);
            bw.Write(nLength);
            bw.Write(ChunkType);
            // WAVE块
            bw.Write(ChunkFmt);
            bw.Write(nFormatChunkLength);
            bw.Write(shPad);
            bw.Write(mWavFormat.Channels);
            bw.Write(mWavFormat.SamplesPerSecond);
            bw.Write(mWavFormat.AverageBytesPerSecond);
            bw.Write(shBytesPerSample);
            bw.Write(mWavFormat.BitsPerSample);
            // 数据块
            bw.Write(ChunkData);
            bw.Write((int)0);   // The sample length will be written in later.
        }

        private bool CreateCaputerDevice()
        {
            //首先要玫举可用的捕捉设备
            CaptureDevicesCollection capturedev = new CaptureDevicesCollection();
            Guid devguid;
            if (capturedev.Count > 0)
            {
                devguid = capturedev[1].DriverGuid;
            }
            else
            {
                MessageBox.Show("当前没有可用于音频捕捉的设备", "系统提示");
                return false;
            }
            //利用设备GUID来建立一个捕捉设备对象
            capture = new Microsoft.DirectX.DirectSound.Capture(devguid);
            return true;
        }
        public void CreateCaptureBuffer()
        {//想要创建一个捕捉缓冲区必须要两个参数：缓冲区信息（描述这个缓冲区中的格式等），缓冲设备。

            CaptureBufferDescription bufferdescription = new CaptureBufferDescription();
            bufferdescription.Format = mWavFormat;//设置缓冲区要捕捉的数据格式
            iNotifySize = 1024;//设置通知大小
            iBufferSize = iNotifyNum * iNotifySize;
            bufferdescription.BufferBytes = iBufferSize;
            capturebuffer = new CaptureBuffer(bufferdescription, capture);//建立设备缓冲区对象
        }
        //设置通知
        private void CreateNotification()
        {
            BufferPositionNotify[] bpn = new BufferPositionNotify[iNotifyNum];//设置缓冲区通知个数
            //设置通知事件
            notifyevent = new AutoResetEvent(false);
            notifythread = new Thread(RecoData);
            notifythread.Start();
            for (int i = 0; i < iNotifyNum; i++)
            {
                bpn[i].Offset = iNotifySize + i * iNotifySize - 1;//设置具体每个的位置
                bpn[i].EventNotifyHandle = notifyevent.Handle;
            }
            myNotify = new Notify(capturebuffer);
            myNotify.SetNotificationPositions(bpn);

        }
        //线程中的事件
        private void RecoData()
        {
            while (true)
            {
                // 等待缓冲区的通知消息
                notifyevent.WaitOne(Timeout.Infinite, true);
                // 录制数据
                RecordCapturedData();
            }
        }

        //真正转移数据的事件，其实就是把数据转移到WAV文件中。
        private void RecordCapturedData()
        {
            byte[] capturedata = null;
            int readpos = 0, capturepos = 0, locksize = 0;
            capturebuffer.GetCurrentPosition(out capturepos, out readpos);
            locksize = readpos - iBufferOffset;//这个大小就是我们可以安全读取的大小
            if (locksize == 0)
            {
                return;
            }
            if (locksize < 0)
            {//因为我们是循环的使用缓冲区，所以有一种情况下为负：当文以载读指针回到第一个通知点，而Ibuffeoffset还在最后一个通知处
                locksize += iBufferSize;
            }

            capturedata = (byte[])capturebuffer.Read(iBufferOffset, typeof(byte), LockFlag.FromWriteCursor, locksize);

            //发送出去
            Thread thread = new Thread(new ParameterizedThreadStart(Send));
            thread.IsBackground = true;
            thread.Start(capturedata);

            iSampleSize += capturedata.Length;
            iBufferOffset += capturedata.Length;
            iBufferOffset %= iBufferSize;//取模是因为缓冲区是循环的。
        }

        public void stoprec()
        {
            //写WAV文件尾
            capturebuffer.Stop();//调用缓冲区的停止方法。停止采集声音
            if (notifyevent != null)
                notifyevent.Set();//关闭通知
            //RecordCapturedData();//将缓冲区最后一部分数据写入到文件中

            //bw.Seek(4, SeekOrigin.Begin);
            //bw.Write((int)(iSampleSize + 36));   // 写文件长度
            //bw.Seek(40, SeekOrigin.Begin);
            //bw.Write(iSampleSize);                // 写数据长度
            //bw.Close();
            //bw.Dispose();
            notifythread.Abort();//结束线程
        }
    }
}
