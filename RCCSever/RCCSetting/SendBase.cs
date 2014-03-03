using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace RCCSever.RCCSetting
{
    public class SendBase
    {
        protected IPEndPoint ipPoint;
        protected int YesNum;
        protected int NoNum;
        public SendBase(IPEndPoint ipPoint, int YesNum, int NoNum)
        {
            this.ipPoint = ipPoint;
            this.YesNum = YesNum;
            this.NoNum = NoNum;
        }

        protected void BeginSend(MemoryStream ms)
        {
            StateObject state = new StateObject();
            state.ms = new MemoryStream(ms.ToArray());
            state.workSocket = new UdpClient();

            ///给每个发送的bytes前面加上他的类型
            byte[] b = new byte[4];
            int lenght = StateObject.lenght - 4;
            int RealLenght = (int)(state.ms.Length - state.ms.Position);
            if (RealLenght > lenght) b = BitConverter.GetBytes(NoNum);
            else
            {
                b = BitConverter.GetBytes(YesNum);
                lenght =RealLenght;
            }
            state.buffer = new byte[lenght + 4];
            for (int i = 0; i < 4; i++)  state.buffer[i] = b[i];
            //准备发送
            state.ms.Read(state.buffer, 4, lenght);
            state.workSocket.BeginSend(state.buffer, state.buffer.Length, ipPoint, new AsyncCallback(SendCallback), state);
        }
        protected void SendCallback(IAsyncResult ar)
        {
            StateObject state = ar.AsyncState as StateObject;
            UdpClient udpClient = state.workSocket;
            MemoryStream ms = state.ms;
            int bytesSent = udpClient.EndSend(ar);

            byte[] bnum = new byte[4];
            for (int i = 0; i < 4; i++) { bnum[i] = state.buffer[i]; }
            int num = BitConverter.ToInt32(bnum, 0);
            ///是否完成
            if (num == YesNum)
            {

            }
            else
            {
                byte[] b = new byte[4];
                int lenght = StateObject.lenght - 4;
                int RealLenght = (int)(state.ms.Length - state.ms.Position);
                if (RealLenght > lenght) b = BitConverter.GetBytes(NoNum);
                else
                {
                    b = BitConverter.GetBytes(YesNum);
                    lenght = RealLenght;
                }
                state.buffer = new byte[lenght + 4];
                for (int i = 0; i < 4; i++)  state.buffer[i] = b[i];
                ms.Read(state.buffer, 4, lenght);
                try
                {
                    udpClient.BeginSend(state.buffer, state.buffer.Length, ipPoint, new AsyncCallback(SendCallback), state);
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
