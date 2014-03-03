using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RCCSever
{
    public class DataBuffer
    {
        public Socket socket;
        public const int MAXSIZE=1024*50;
        public const int MINSIZE = 20;
        public byte[] data = new byte[MAXSIZE];
        public EndPoint endpoint;
    }
}
