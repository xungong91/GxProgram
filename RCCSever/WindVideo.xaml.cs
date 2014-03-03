using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.DirectX.DirectSound;
using RCCSever.RCCSetting;

namespace RCCSever
{
    public delegate void DelegateWriteTextBox(string str);
    public delegate void DelegateWriteImage(BitmapImage bitimg);
    public delegate void DelegatePlayAudio(MemoryStream ms);
    /// <summary>
    /// WindVideo.xaml 的交互逻辑
    /// </summary>
    public partial class WindVideo 
    {
        private RCCSever.RCCAPI.Capture cam = null;
        DispatcherTimer timer;
        private IntPtr wpfHwnd;
        // 定义UDP发送和接收
        private UdpClient udpReceive = null;
        // 定义节点
        private IPEndPoint ipEndPoint = null;
        private IPEndPoint remoteEP = null;
        private static WindVideo _WindVideo;
        private WindVideo()
        {
            InitializeComponent();
            //摄像头
            cam = new RCCSever.RCCAPI.Capture(0, 10, 320, 240);
            cam.Start();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += StartPlayVideo;  //你的事件
            IPAddress localIP = null;
            try
            {
                foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                    {
                        localIP = _IPAddress;
                    }
                }
            }
            catch
            {
                localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[3];
            };
            int localport = Convert.ToInt32(ConfigurationManager.AppSettings["LocalPort"]);
            ipEndPoint = new IPEndPoint(localIP, localport);
            udpReceive = new UdpClient(ipEndPoint);
            //udpSend = new UdpClient(remoteEP);
            StateObject state = new StateObject();
            state.workSocket = udpReceive;
            udpReceive.BeginReceive(new AsyncCallback(ReceiveCallback), state);
        }
        public static WindVideo getInstance()
        {
            if (_WindVideo==null)
            {
                _WindVideo=new WindVideo();
            }
            return _WindVideo;
        }
        private void MetroWindow_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer.Stop();
            cam.Dispose();
            udpReceive.Close();
            _WindVideo = null;
        }
        //连接
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            IPAddress remoteIP;
            if (IPAddress.TryParse(iptxt.Text, out remoteIP) == false)
            {
                MessageBox.Show("远程IP格式不正确");
                return;
            }
            remoteEP = new IPEndPoint(remoteIP, Convert.ToInt32(porttxt.Text));
        }
       
        /// 视频
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
        private void StartPlayVideo(object sender, EventArgs e)
        {
            SendVideo sv = new SendVideo(remoteEP, 21, 20);
            sv.Send(Toimg, cam);
            //timer.Stop();
        }
        /// 语音
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SendAudio sa = new SendAudio(remoteEP, 31, 30);
            sa.Start();
        }
        /// 文字
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SendText sendText = new SendText(remoteEP, 11, 10);
            sendText.Send(txtsend.Text);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = ar.AsyncState as StateObject;
            UdpClient udpClient = state.workSocket;
            IPEndPoint remote = null;
            Byte[] data;
            try{ data = udpClient.EndReceive(ar, ref remote);}
            catch{return;}

            //查看是什么类型
            byte[] bnum = new byte[4];
            for (int i = 0; i < 4; i++) { bnum[i] = data[i]; }
            int num = BitConverter.ToInt32(bnum, 0);
            switch (num)
            {
                case 10:
                    state.mstext.Write(data, 4, data.Length - 4);
                    break;
                case 11:
                    state.mstext.Write(data, 4, data.Length - 4);
                    this.Dispatcher.BeginInvoke(new DelegateWriteTextBox(WriteTextBox), SendText.Reveive(state.mstext));
                    state.mstext = new MemoryStream();
                    break;
                case 20:
                    state.ms.Write(data, 4, data.Length - 4);
                    break;
                case 21:
                    state.ms.Write(data, 4, data.Length - 4);
                    this.Dispatcher.BeginInvoke(new DelegateWriteImage(WriteImage), SendVideo.Reveive(state.ms));
                    state.ms = new MemoryStream();
                    break;
                case 30:
                    state.msvoice.Write(data, 4, data.Length - 4);
                    break;
                case 31:
                    state.msvoice.Write(data, 4, data.Length - 4);
                    this.Dispatcher.BeginInvoke(new DelegatePlayAudio(PlayAudio), SendAudio.Receive(state.msvoice));
                    state.msvoice = new MemoryStream();
                    break;
                default:
                    break;
            }
            try
            {
                state.buffer = new byte[StateObject.lenght];
                udpReceive.BeginReceive(new AsyncCallback(ReceiveCallback), state);
            }
            catch
            {
                throw;
            }
        }
        private void WriteImage(BitmapImage bitimg)
        {
            if (bitimg != null)
            {
                Fromimg.Source = bitimg;
            }
            else
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                string filename = AppDomain.CurrentDomain.BaseDirectory + "two.jpg";
                image.UriSource = new Uri(filename);
                image.EndInit();
                Fromimg.Source = image;
            }
        }
        private void WriteTextBox(string text)
        {
            txtReceive.Text = text;
        }
        private BufferDescription buffDes;
        private void InitAudio()
        {
            DevicesCollection dcollection = new DevicesCollection();
            Guid devguid = dcollection[1].DriverGuid;
            device = new Microsoft.DirectX.DirectSound.Device(devguid);
            device.SetCooperativeLevel(wpfHwnd, CooperativeLevel.Normal);
        }
        Microsoft.DirectX.DirectSound.Device device;
        public void PlayAudio(MemoryStream ms)
        {
            try
            {
                buffDes = new BufferDescription(SendAudio.SetWaveFormat());
                buffDes.GlobalFocus = true;//设置缓冲区全局获取焦点
                buffDes.ControlVolume = true;//指明缓冲区可以控制声音
                buffDes.ControlPan = true;//指明缓冲区可以控制声道平衡
                buffDes.BufferBytes = (int)ms.Length;

                MemoryStream newms = new MemoryStream(ms.ToArray());
                SecondaryBuffer sec = new SecondaryBuffer(newms, buffDes, device);
                //newms.Seek(0, SeekOrigin.Begin);
                //sec.Write(0,newms, (int)ms.Length, LockFlag.FromWriteCursor);
                sec.Play(0, BufferPlayFlags.Default);
            }
            catch
            {

            }
        }

        private void MetroWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            //iptxt.Text = GetIP();
        }
        private string GetIP()
        {
            Uri uri = new Uri("http://www.ikaka.com/ip/index.asp");
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = 0;
            req.CookieContainer = new System.Net.CookieContainer();
            req.GetRequestStream().Write(new byte[0], 0, 0);
            System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)(req.GetResponse());
            StreamReader rs = new StreamReader(res.GetResponseStream(), System.Text.Encoding.GetEncoding("GB18030"));
            string s = rs.ReadToEnd();
            rs.Close();
            req.Abort();
            res.Close();
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(s, @"\d+.\d+.\d+.\d+");
            if (m.Success) return m.Groups[0].Value;
            return string.Empty;
        }
    }
}
