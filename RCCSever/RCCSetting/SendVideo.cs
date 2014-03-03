using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using RCCSever.RCCAPI;

namespace RCCSever.RCCSetting
{
    public class SendVideo:SendBase
    {
        public SendVideo(IPEndPoint ipPoint, int YesNum, int NoNum)
            : base(ipPoint, YesNum, NoNum)
        {
            
        }
        public void Send(System.Windows.Controls.Image imgge, Capture cam)
        {
            MemoryStream ms = new MemoryStream();
            Bitmap bitmap = null;
            IntPtr ip = IntPtr.Zero;
            ip = cam.GetBitMap();
            bitmap = new Bitmap(cam.Width, cam.Height, cam.Stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, ip);
            //旋转
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

            //不安全的刷新方式
            try
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(ms.ToArray());
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                imgge.Source = bitmapImage;
            }
            catch (Exception ex)
            {
                throw;
            }
            //释放指针ip
            Marshal.FreeCoTaskMem(ip);
            //回收垃圾
            GC.Collect();

            base.BeginSend(ms);
        }
        public static BitmapImage Reveive(MemoryStream ms)
        {
            BitmapImage bitmapImage = new BitmapImage();
            try
            {
                //测试
                /*
                byte[] a = ms.ToArray();
                FileStream fs = new FileStream(AppDomain.CurrentDomain.DynamicDirectory + "photos\\" + DateTime.Now.ToString("hhmmssffff") + ".jpg", FileMode.Create);
                fs.Write(a, 0, a.Length);
                fs.Close();
                fs.Dispose();
                */
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(ms.ToArray());
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
            catch
            {
                return null;
            }
        }
    }
}
