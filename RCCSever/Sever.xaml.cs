using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using RCCSever.RCCSetting;
using RCCSever.RCCSocket;

namespace RCCSever
{
    /// <summary>
    /// Sever.xaml 的交互逻辑
    /// </summary>
    public partial class Sever 
    {
        public Sever()
        {
            InitializeComponent();
        }
        private void start()
        {
            StartListening();
            //SocketHelper sh = new SocketHelper();
            //sh.ReceiveNetWork("000000");
        }
        private void end()
        {
            tcpThread.Abort();
            udpThread.Abort();
            tcpListener.Close();
            udpSocket.Close();
        }

        private void MetroWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            BtnLien.Click += BtnLien_Click;
        }

        void BtnLien_Click(object sender, RoutedEventArgs e)
        {
            if (BtnLien.IsChecked == true)
                start();
            else
                end();
        }

        private void btnvideo(object sender, RoutedEventArgs e)
        {
            WindVideo.getInstance().Show();
        }
        private void btnsetting(object sender, RoutedEventArgs e)
        {
            var flyout = this.Flyouts.Items[0] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }
        private void btncreate(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("确定");
        }


        #region socket
        private ManualResetEvent allDone = new ManualResetEvent(false);
        private Socket tcpListener;
        private Socket udpSocket;
        private bool m_TcpListening = true;
        private Thread tcpThread;
        private Thread udpThread;
        private bool isUdpListening = true;
        private void StartListening()
        {
            //开启udp监听
            ThreadStart start = new ThreadStart(UdpListen);
            udpThread = new Thread(start);
            udpThread.Start();

            //开启tcp监听
            ThreadStart start2 = new ThreadStart(TcpListen);
            tcpThread = new Thread(start2);
            tcpThread.Start();
        }


        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8081);
        private IPAddress GetLocalIP()
        {
            System.Net.IPAddress[] ips = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
            IPAddress result = null;
            foreach (IPAddress ip in ips)
            {
                if (!ip.IsIPv6LinkLocal)
                {
                    return ip;
                }
            }
            return result;
        }

        private void TcpListen()
        {
            tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                tcpListener.Bind(localEndPoint);
                tcpListener.Listen(10);

                while (m_TcpListening)
                {
                    tcpListener.BeginAccept(BUFFERSIZE, new AsyncCallback(AcceptCallback), null);
                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void UdpListen()
        {
            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            EndPoint ePoint;
            try
            {
                ePoint = new IPEndPoint(IPAddress.Any, 65432);
                udpSocket.Bind(ePoint);

                while (isUdpListening)
                {
                    int l = udpSocket.ReceiveFrom(buffer, ref ePoint);
                    if (ePoint != null)
                    {
                        string ip = GetLocalIP().ToString();
                        IPEndPoint p = (IPEndPoint)ePoint;
                        p.Port = 8081;
                        int i = udpSocket.SendTo(System.Text.Encoding.Default.GetBytes(ip), p);

                        //if (i>0)
                        //{
                        //    //udpThread.Abort();
                        //}

                    }
                }


            }
            catch (Exception)
            {

                throw;
            }

        }
        private const int BUFFERSIZE = 65535;
        private byte[] buffer = new byte[BUFFERSIZE];
        public Socket recSocket;
        private void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            //allDone.Set();
            try
            {
                byte[] buffertemp = new byte[BUFFERSIZE];
                recSocket = tcpListener.EndAccept(ar);
                recSocket.BeginReceive(buffertemp, 0, BUFFERSIZE, SocketFlags.None, new AsyncCallback(ReceiveHandle), buffertemp);
                this.Dispatcher.BeginInvoke(new SetTipsTextDelegate(SetTipsText), "已连接");
            }
            catch
            {
            }
        }

        private delegate void SetTipsTextDelegate(string pTipsText);
        private void SetTipsText(string pTipTxet)
        {
            lbTips.Content = pTipTxet;
            Progress1.Visibility = Visibility.Collapsed;
        }
        private void ReceiveHandle(IAsyncResult ia)
        {
            try
            {
                int i = recSocket.EndReceive(ia);
                if (i > 0)
                {
                    Dispatcher.BeginInvoke(new mydelegate(ExecuteCommand), (byte[])ia.AsyncState);
                }
            }
            catch (Exception e)
            {
                //throw;
            }

        }

        private delegate void mydelegate(object obj);
        private void ExecuteCommand(object obj)
        {
            if (obj is byte[])
            {
                byte[] buffertemp = obj as byte[];
                if (buffertemp.Count() > 0)
                {
                    //if (BitConverter.IsLittleEndian)
                    //    Array.Reverse(buffertemp);
                    StreamType type = (StreamType)BitConverter.ToInt32(new byte[] { buffertemp[0], buffertemp[1], buffertemp[2], buffertemp[3] }, 0);

                    int command = BitConverter.ToInt32(new byte[] { buffertemp[4], buffertemp[5], buffertemp[6], buffertemp[7] }, 0);
                    switch (type)
                    {
                        case StreamType.CommandSystem:
                            SysCommand sysCommand = (SysCommand)command;
                            switch (sysCommand)
                            {
                                case SysCommand.ShutDown:
                                    SysCommandManager.ShutDownS(30);
                                    break;
                                case SysCommand.Dormancy:
                                    SysCommandManager.Dormancy();
                                    break;
                                case SysCommand.VolumeDown:
                                    SysCommandManager.VolumeDown();
                                    break;
                                case SysCommand.VolumeUp:
                                    SysCommandManager.VolumeUp();
                                    break;
                                case SysCommand.VolumeMute:
                                    SysCommandManager.VolumeMute();
                                    break;
                                case SysCommand.CancelShutDown:
                                    SysCommandManager.CancelShutDown();
                                    break;
                                case SysCommand.Restart:
                                    SysCommandManager.Restart();
                                    break;
                            }
                            break;
                        case StreamType.CommandPPT:
                            PPTCommand pptCommand = (PPTCommand)command;
                            PPTCommandManager manager = PPTCommandManager.GetInstance();
                            switch (pptCommand)
                            {
                                case PPTCommand.Start:
                                    manager.Start();
                                    break;
                                case PPTCommand.Next:
                                    manager.Next();
                                    break;
                                case PPTCommand.Last:
                                    manager.Last();
                                    break;
                                case PPTCommand.Exit:
                                    manager.Exit();
                                    break;
                            }
                            break;
                        case StreamType.CommandAV:
                            AVCommand avCommand = (AVCommand)command;
                            AVCommandManager avCommandManager = AVCommandManager.GetInstance();
                            switch (avCommand)
                            {
                                    /*
                                case AVCommand.AVStart:
                                    if (FrmAV.GetInstance() == null)
                                    {
                                        FrmAV.GetInstance(FrmAV.FormType.Callee).Show();
                                    }
                                    break;
                                case AVCommand.AVAccept:
                                    if (FrmAV.GetInstance() != null)
                                    {
                                        avCommandManager.BeginMonitorWebcam(FrmAV.GetInstance().avLocalhost);
                                    }
                                    break;
                                case AVCommand.AVRefuse:
                                    if (FrmAV.GetInstance() != null)
                                    {
                                        FrmAV.GetInstance().lbTips.Text = "对方拒绝了您的请求";
                                    }
                                    break;
                                case AVCommand.AVSuspend:
                                    if (FrmAV.GetInstance() != null)
                                    {
                                        FrmAV.GetInstance().lbTips.Text = "对方挂断了视频";
                                    }
                                    break;
                                     */
                            }
                            break;
                        case StreamType.VideoStream:
                            /*
                            byte[] bytes2 = new byte[buffertemp.Length - 8];
                            Buffer.BlockCopy(buffertemp, 8, bytes2, 0, buffertemp.Length - 8);
                            if (FrmAV.GetInstance() != null)
                            {
                                MemoryStream stream = new MemoryStream(bytes2);

                                FrmAV.GetInstance().avPhone.Image = System.Drawing.Image.FromStream(stream);
                            }
                            else
                            {
                                FrmAV.GetInstance(FrmAV.FormType.Callee).Show();
                                MemoryStream stream = new MemoryStream(bytes2);

                                FrmAV.GetInstance().avPhone.Image = System.Drawing.Image.FromStream(stream);
                            }
                            */
                            break;
                    }
                }

            }
            byte[] buffertemp2 = new byte[BUFFERSIZE];
            //recSocket = tcpListener.EndAccept(ar);
            recSocket.BeginReceive(buffertemp2, 0, BUFFERSIZE, SocketFlags.None, new AsyncCallback(ReceiveHandle), buffertemp2);


        }
        #endregion
    }
}
