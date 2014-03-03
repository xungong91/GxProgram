using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace RCCSever.RCCSocket
{
    public class SocketHelper
    {
        private string password;
        private Socket udp;
        private Socket tcp;
        public void ReceiveNetWork(string password)
        {
            this.password = password;
            udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 65432);
            udp.Bind(iep);
            //接受广播
            Thread th = new Thread(new ThreadStart(ThreadUdpCb)){IsBackground=true};
            th.Start();
            //监听
            Thread th1 = new Thread(new ThreadStart(tcplien)) { IsBackground = true };
            th1.Start();
        }
        private void ThreadUdpCb()
        {
            byte[] a=new byte[20];
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 65432);
            EndPoint ep = iep;
            int lenth = udp.ReceiveFrom(a, ref ep);
            string data = ASCIIEncoding.Default.GetString(a, 0, lenth);
            if (data == password)
            {
                string localip = GetLocalIP();
                byte[] localipc = ASCIIEncoding.Default.GetBytes(localip);
                IPEndPoint sendiep = (IPEndPoint)ep;
                sendiep.Port = 8081;
                udp.SendTo(localipc, sendiep);
            }
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
        private string GetLocalIP()
        {
            IPAddress[] ips= Dns.GetHostAddresses(Dns.GetHostName());
            return ips[1].ToString();
        }
        private void tcplien()
        {
            tcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcp.Bind(new IPEndPoint(IPAddress.Any, 8081));
            tcp.Listen(2);
            Socket socket= tcp.Accept();
            DataBuffer db=new DataBuffer();
            db.socket=socket;
            socket.BeginReceive(db.data, 0, DataBuffer.MAXSIZE, SocketFlags.None, new AsyncCallback(ReceiveCB), db);
        }
        private void ReceiveCB(IAsyncResult ar)
        {
            DataBuffer db = ar.AsyncState as DataBuffer;
            int length= db.socket.EndReceive(ar);
            DataBuffer dbb = new DataBuffer();
            dbb.socket = db.socket;
            dbb.socket.BeginReceive(dbb.data, 0, DataBuffer.MAXSIZE, SocketFlags.None, new AsyncCallback(ReceiveCB), dbb);
        }
    }
}
