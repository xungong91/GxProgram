using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using CoreAudioApi;

namespace RCCSever.RCCSetting
{
    public enum StreamType
    {
        CommandSystem = 0,
        CommandMedia,
        CommandPPT,
        CommandAV,
        AudioStream,
        VideoStream
    }
    public enum SysCommand
    {
        VolumeUp = 0,
        VolumeDown,
        VolumeMute,
        ShutDown,
        CancelShutDown,
        Restart,
        Dormancy,
        Webcam
    }
    public enum PPTCommand
    {
        Start = 0,
        Next,
        Last,
        Exit
    }
    public enum AVCommand
    {
        AVStart = 0,
        AVRefuse,
        AVAccept,
        AVSuspend
    }
    public enum Key
    {
        //      鼠标动代码:
        move = 0x0001,
        leftdown = 0x0002,
        leftup = 0x0004,
        rightdown = 0x0008,
        rightup = 0x0010,
        middledown = 0x0020,
        //键盘动作代码:
        VK_LBUTTON = 1,      //鼠标左键 
        VK_RBUTTON = 2,　    //鼠标右键 
        VK_CANCEL = 3,　　　 //Ctrl+Break(通常不需要处理) 
        VK_MBUTTON = 4,　　  //鼠标中键 
        VK_BACK = 8, 　　　  //Backspace 
        VK_TAB = 9,　　　　  //Tab 
        VK_CLEAR = 12,　　　 //Num Lock关闭时的数字键盘5 
        VK_RETURN = 13,　　　//Enter(或者另一个) 
        VK_SHIFT = 16,　　　 //Shift(或者另一个) 
        VK_CONTROL = 17,　　 //Ctrl(或者另一个） 
        VK_MENU = 18,　　　　//Alt(或者另一个) 
        VK_PAUSE = 19,　　　 //Pause 
        VK_CAPITAL = 20,　　 //Caps Lock 
        VK_ESCAPE = 27,　　　//Esc 
        VK_SPACE = 32,　　　 //Spacebar 
        VK_PRIOR = 33,　　　 //Page Up 
        VK_NEXT = 34,　　　　//Page Down 
        VK_END = 35,　　　　 //End 
        VK_HOME = 36,　　　　//Home 
        VK_LEFT = 37,　　　  //左箭头 
        VK_UP = 38,　　　　  //上箭头 
        VK_RIGHT = 39,　　　 //右箭头 
        VK_DOWN = 40,　　　  //下箭头 
        VK_SELECT = 41,　　  //可选 
        VK_PRINT = 42,　　　 //可选 
        VK_EXECUTE = 43,　　 //可选 
        VK_SNAPSHOT = 44,　　//Print Screen 
        VK_INSERT = 45,　　　//Insert 
        VK_DELETE = 46,　　  //Delete 
        VK_HELP = 47,　　    //可选 
        VK_NUM0 = 48,        //0
        VK_NUM1 = 49,        //1
        VK_NUM2 = 50,        //2
        VK_NUM3 = 51,        //3
        VK_NUM4 = 52,        //4
        VK_NUM5 = 53,        //5
        VK_NUM6 = 54,        //6
        VK_NUM7 = 55,        //7
        VK_NUM8 = 56,        //8
        VK_NUM9 = 57,        //9
        VK_A = 65,          //A
        VK_B = 66,          //B
        VK_C = 67,          //C
        VK_D = 68,          //D
        VK_E = 69,          //E
        VK_F = 70,          //F
        VK_G = 71,          //G
        VK_H = 72,          //H
        VK_I = 73,          //I
        VK_J = 74,          //J
        VK_K = 75,          //K
        VK_L = 76,          //L
        VK_M = 77,          //M
        VK_N = 78,          //N
        VK_O = 79,          //O
        VK_P = 80,          //P
        VK_Q = 81,          //Q
        VK_R = 82,          //R
        VK_S = 83,          //S
        VK_T = 84,          //T
        VK_U = 85,          //U
        VK_V = 86,          //V
        VK_W = 87,          //W
        VK_X = 88,          //X
        VK_Y = 89,          //Y
        VK_Z = 90,          //Z
        VK_NUMPAD0 = 96,    //0
        VK_NUMPAD1 = 97,    //1
        VK_NUMPAD2 = 98,    //2
        VK_NUMPAD3 = 99,    //3
        VK_NUMPAD4 = 100,    //4
        VK_NUMPAD5 = 101,    //5
        VK_NUMPAD6 = 102,    //6
        VK_NUMPAD7 = 103,    //7
        VK_NUMPAD8 = 104,    //8
        VK_NUMPAD9 = 105,    //9
        VK_NULTIPLY = 106,　 //数字键盘上的* 
        VK_ADD = 107,　　　　//数字键盘上的+ 
        VK_SEPARATOR = 108,　//可选 
        VK_SUBTRACT = 109,　 //数字键盘上的- 
        VK_DECIMAL = 110,　　//数字键盘上的. 
        VK_DIVIDE = 111,　　 //数字键盘上的/
        VK_F1 = 112,
        VK_F2 = 113,
        VK_F3 = 114,
        VK_F4 = 115,
        VK_F5 = 116,
        VK_F6 = 117,
        VK_F7 = 118,
        VK_F8 = 119,
        VK_F9 = 120,
        VK_F10 = 121,
        VK_F11 = 122,
        VK_F12 = 123,
        VK_NUMLOCK = 144,　　//Num Lock 
        VK_SCROLL = 145, 　  // Scroll Lock 
        middleup = 0x0040,
        xdown = 0x0080,
        xup = 0x0100,
        wheel = 0x0800,
        virtualdesk = 0x4000,
        absolute = 0x8000
    }
    /// <summary>
    /// ppt控制器
    /// </summary>
    public class PPTCommandManager
    {
        //获取当前窗口句柄
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        //触发键盘消息
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        IntPtr currentPPTHandle;

        private const uint keydown = 0;
        private const uint keyup = 0x2;
        private static PPTCommandManager _instance;
        private static bool isFullScreeen;
        public static PPTCommandManager GetInstance()
        {
            if (_instance == null || !isFullScreeen)
            {
                _instance = new PPTCommandManager();
            }
            return _instance;
        }
        public PPTCommandManager()
        {
            Process[] processes = Process.GetProcesses();
            currentPPTHandle = GetForegroundWindow();
            foreach (Process process in processes)
            {
                try
                {
                    if (process.MainWindowHandle.Equals(currentPPTHandle))
                    {
                        if (!process.ProcessName.Equals("POWERPNT"))
                        {
                            currentPPTHandle = IntPtr.Zero;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }
        public void Start()
        {
            if (currentPPTHandle != IntPtr.Zero)
            {
                keybd_event((byte)(Key.VK_F5), 0, keydown, 0);
                isFullScreeen = true;
            }
        }
        public void Next()
        {
            if (currentPPTHandle != IntPtr.Zero)
            {
                keybd_event((byte)(Key.VK_RIGHT), 0, keydown, 0);
            }
        }
        public void Last()
        {
            if (currentPPTHandle != IntPtr.Zero)
            {
                keybd_event((byte)(Key.VK_LEFT), 0, keydown, 0);
            }
        }
        public void Exit()
        {
            if (currentPPTHandle != IntPtr.Zero)
            {
                keybd_event((byte)(Key.VK_ESCAPE), 0, keydown, 0);
                isFullScreeen = false;
            }
        }
    }
    public class SysCommandManager
    {
        /// <summary>
        /// 执行cmd命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern int system(string cmd);

        private static MMDevice device;
        public static MMDevice GetDeciceInstance()
        {
            if (device == null)
            {
                MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
                device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            }
            return device;
        }
        /// <summary>
        /// 增大音量
        /// </summary>
        public static void VolumeUp()
        {
            if (GetDeciceInstance().AudioEndpointVolume.MasterVolumeLevelScalar <= 0.95)
            {
                GetDeciceInstance().AudioEndpointVolume.MasterVolumeLevelScalar += 0.05f;
            }
            else
            {
                GetDeciceInstance().AudioEndpointVolume.MasterVolumeLevelScalar = 1.0f;
            }
        }
        /// <summary>
        /// 减小音量
        /// </summary>
        public static void VolumeDown()
        {
            if (GetDeciceInstance().AudioEndpointVolume.MasterVolumeLevelScalar >= 0.05)
            {
                GetDeciceInstance().AudioEndpointVolume.MasterVolumeLevelScalar -= 0.05f;
            }
            else
            {
                GetDeciceInstance().AudioEndpointVolume.MasterVolumeLevelScalar = 0.0f;
            }
        }
        /// <summary>
        /// 静音，执行两次恢复
        /// </summary>
        public static void VolumeMute()
        {
            if (GetDeciceInstance().AudioEndpointVolume.Mute)
            {
                GetDeciceInstance().AudioEndpointVolume.Mute = false;
            }
            else
            {
                GetDeciceInstance().AudioEndpointVolume.Mute = true;
            }

        }
        public static void ShutDownS(int pTime)
        {
            system("shutdown /s /t " + pTime.ToString());
        }
        public static void Dormancy()
        {
            system("shutdown /h");
        }
        public static void Restart()
        {
            system("shutdown /r");
        }
        public static void CancelShutDown()
        {
            system("shutdown /a");
        }
    }
    public class AVCommandManager
    {
        public static AVCommandManager _instance;
        public static AVCommandManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AVCommandManager();
            }
            return _instance;
        }
        private AVCommandManager()
        {

        }
        //private VideoCapture videocam;
        //public void BeginMonitorWebcam(System.Windows.Forms.Control hControl)
        //{
        //    const int VIDEODEVICE = 0; // zero based index of video capture device to use
        //    const int VIDEOWIDTH = 480; // Depends on video device caps
        //    const int VIDEOHEIGHT = 320; // Depends on video device caps
        //    const int VIDEOBITSPERPIXEL = 24; // BitsPerPixel values determined by device

        //    videocam = new VideoCapture(VIDEODEVICE, VIDEOWIDTH, VIDEOHEIGHT, VIDEOBITSPERPIXEL, hControl);
        //    videocam.StreamCallBack += new VideoCapture.StreamCallBackEvenHandle(Video_StreamCallBack);
        //}

        //private void Video_StreamCallBack(object sender, IntPtr pBuffer, int BufferLen)
        //{
        //    byte[] bytes = new byte[BufferLen];
        //    Marshal.Copy(pBuffer, bytes, 0, BufferLen);

        //    byte[] bytejpg = Helpers.ImageConverter.BmpToJpegBuff(bytes);
        //    byte[] bytesToSend = new byte[bytejpg.Length + 8];
        //    Buffer.BlockCopy(bytejpg, 0, bytesToSend, 8, bytejpg.Length + 8);
        //    bytesToSend[3] = 5;
        //    MainWindow.GetInstance().recSocket.Send(bytesToSend);
        //}
    }
}
