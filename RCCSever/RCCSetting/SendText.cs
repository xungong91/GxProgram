using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace RCCSever.RCCSetting
{
    public class SendText:SendBase
    {
        public SendText(IPEndPoint ipPoint, int YesNum, int NoNum)
            : base(ipPoint, YesNum, NoNum)
        {
            
        }
        public void Send(string Text)
        {
            MemoryStream ms = new MemoryStream();
            byte[] buffer = ASCIIEncoding.Default.GetBytes(Text);
            ms.Write(buffer, 0, buffer.Length);
            base.BeginSend(ms);
        }
        public static string Reveive(MemoryStream ms)
        {
            byte[] data=ms.ToArray();
            string result = ASCIIEncoding.Default.GetString(data);
            return result;
        }
    }
}
