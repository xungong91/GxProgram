using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using Test.Native;

namespace Test
{
    public class TestWindow:Window
    {
        private IntPtr _mHWND;
        private HwndSource _hwndSource;
        public TestWindow()
        {
            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = AllowsTransparency;
            Loaded += TestWindow_Loaded;
        }
        void TestWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (_hwndSource != null)
            {
                _hwndSource.AddHook(HwndHook);
            }
            _mHWND = new WindowInteropHelper(this).Handle;
        }

        private IntPtr HwndHook(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            IntPtr returnval = IntPtr.Zero;
            switch (message)
            {
                case Constants.WM_NCCALCSIZE:
                    //handled = true;
                    break;
                #region
                
                case Constants.WM_NCPAINT:
                    if (ShouldHaveBorder())
                    {
                    }
                    else if (true)
                    {
                        var val = 2;
                        UnsafeNativeMethods.DwmSetWindowAttribute(_mHWND, 2, ref val, 4);
                        var m = new MARGINS { bottomHeight = 1, leftWidth = 1, rightWidth = 1, topHeight = 1 };
                        UnsafeNativeMethods.DwmExtendFrameIntoClientArea(_mHWND, ref m);
                    }
                    handled = true;
                    break;
                case Constants.WM_NCACTIVATE:
                    returnval = UnsafeNativeMethods.DefWindowProc(hWnd, message, wParam, new IntPtr(-1));
                    handled = true;
                    break;
                case Constants.WM_GETMINMAXINFO:
                     handled = false;
                    break;
                case Constants.WM_NCHITTEST:
                    // don't process the message on windows that can't be resized
                    var resizeMode = this.ResizeMode;
                    if (resizeMode == ResizeMode.CanMinimize || resizeMode == ResizeMode.NoResize || this.WindowState == WindowState.Maximized)
                        break;

                    // get X & Y out of the message                   
                    var screenPoint = new Point(UnsafeNativeMethods.GET_X_LPARAM(lParam), UnsafeNativeMethods.GET_Y_LPARAM(lParam));

                    // convert to window coordinates
                    var windowPoint = this.PointFromScreen(screenPoint);
                    var windowSize = this.RenderSize;
                    var windowRect = new Rect(windowSize);
                    windowRect.Inflate(-6, -6);

                    // don't process the message if the mouse is outside the 6px resize border
                    if (windowRect.Contains(windowPoint))
                        break;

                    var windowHeight = (int)windowSize.Height;
                    var windowWidth = (int)windowSize.Width;

                    // create the rectangles where resize arrows are shown
                    var topLeft = new Rect(0, 0, 6, 6);
                    var top = new Rect(6, 0, windowWidth - 12, 6);
                    var topRight = new Rect(windowWidth - 6, 0, 6, 6);

                    var left = new Rect(0, 6, 6, windowHeight - 12);
                    var right = new Rect(windowWidth - 6, 6, 6, windowHeight - 12);

                    var bottomLeft = new Rect(0, windowHeight - 6, 6, 6);
                    var bottom = new Rect(6, windowHeight - 6, windowWidth - 12, 6);
                    var bottomRight = new Rect(windowWidth - 6, windowHeight - 6, 6, 6);

                    // check if the mouse is within one of the rectangles
                    if (topLeft.Contains(windowPoint))
                        returnval = (IntPtr)Constants.HTTOPLEFT;
                    else if (top.Contains(windowPoint))
                        returnval = (IntPtr)Constants.HTTOP;
                    else if (topRight.Contains(windowPoint))
                        returnval = (IntPtr)Constants.HTTOPRIGHT;
                    else if (left.Contains(windowPoint))
                        returnval = (IntPtr)Constants.HTLEFT;
                    else if (right.Contains(windowPoint))
                        returnval = (IntPtr)Constants.HTRIGHT;
                    else if (bottomLeft.Contains(windowPoint))
                        returnval = (IntPtr)Constants.HTBOTTOMLEFT;
                    else if (bottom.Contains(windowPoint))
                        returnval = (IntPtr)Constants.HTBOTTOM;
                    else if (bottomRight.Contains(windowPoint))
                        returnval = (IntPtr)Constants.HTBOTTOMRIGHT;

                    if (returnval != IntPtr.Zero)
                        handled = true;

                    break;
                default:
                    break;
                #endregion
            }
            return returnval;
        }
        private bool ShouldHaveBorder()
        {
            if (Environment.OSVersion.Version.Major < 6)
                return true;
            return !UnsafeNativeMethods.DwmIsCompositionEnabled();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
