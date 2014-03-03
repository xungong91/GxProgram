using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Native
{
    public static class Constants
    {
        public const int WM_SIZE= 0x05;
        /// <summary>
        /// 当某个窗口的客户区域必须被核算时发送此消息
        /// </summary>
        public const int WM_NCCALCSIZE = 0x83;
        /// <summary>
        /// 移动鼠标，按住或释放鼠标时发生
        /// </summary>
        public const int WM_NCHITTEST = 0x84;
        /// <summary>
        /// 程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时
        /// </summary>
        public const int WM_NCPAINT = 0x85;
        /// <summary>
        /// 此消息发送给某个窗口仅当它的非客户区需要被改变来显示是激活还是非激活状态
        /// </summary>
        public const int WM_NCACTIVATE = 0x86;
        /// <summary>
        /// 此消息发送给窗口当它将要改变大小或位置
        /// </summary>
        public const int WM_GETMINMAXINFO = 0x24;
        /// <summary>
        /// 当一个菜单将要被激活时发送此消息，它发生在用户菜单条中的某项或按下某个菜单键，它允许程序在显示前更改菜单
        /// </summary>
        public const int WM_INITMENU = 0x116;

        public const int HTLEFT = 0x0A;
        public const int HTRIGHT = 0x0B;
        public const int HTTOP = 0x0C;
        public const int HTTOPLEFT = 0x0D;
        public const int HTTOPRIGHT = 0x0E;
        public const int HTBOTTOM = 0x0F;
        public const int HTBOTTOMLEFT = 0x10;
        public const int HTBOTTOMRIGHT = 0x11;
    }
}
