using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Radius
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            // 获取窗体句柄  
            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            // 获得窗体的 样式  
            long oldstyle = NativeMethods.GetWindowLong(hwnd, NativeMethods.GWL_STYLE);
            // 更改窗体的样式为无边框窗体  
            NativeMethods.SetWindowLong(hwnd, NativeMethods.GWL_STYLE, oldstyle & ~NativeMethods.WS_CAPTION);
            // SetWindowLong(hwnd, GWL_EXSTYLE, oldstyle & ~WS_EX_LAYERED);  
            // 1 | 2 << 8 | 3 << 16  r=1,g=2,b=3 详见winuse.h文件  
            // 设置窗体为透明窗体  
            NativeMethods.SetLayeredWindowAttributes(hwnd, 1 | 2 << 8 | 3 << 16, 0, NativeMethods.LWA_ALPHA);
            // 创建圆角窗体  12 这个值可以根据自身项目进行设置  
            NativeMethods.SetWindowRgn(hwnd, NativeMethods.CreateRoundRectRgn(0, 0, Convert.ToInt32(this.ActualWidth), Convert.ToInt32(this.ActualHeight), 12, 12), true);  
        }

        private void Window_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {

            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            // 创建圆角窗体  
            NativeMethods.SetWindowRgn(hwnd, NativeMethods.CreateRoundRectRgn(0, 0, Convert.ToInt32(this.ActualWidth), Convert.ToInt32(this.ActualHeight), 12, 12), true);  

        }
    }
}
