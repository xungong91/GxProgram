using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RCCSever.RCCSetting
{
    public class StateObject
    {
        public UdpClient workSocket = null;
        /// <summary>
        /// 前面4字节 十分位表示type 1,文字 2,视频 3,音频  个位表示状态 1,接受完成 0,正在接受
        /// </summary>
        public byte[] buffer = new byte[lenght];

        public MemoryStream ms = new MemoryStream();

        public MemoryStream msvoice = new MemoryStream();

        public MemoryStream mstext = new MemoryStream();
        public const int lenght = 1024 * 30 + 4;

        public Object thisLock = new Object();
    }
}
