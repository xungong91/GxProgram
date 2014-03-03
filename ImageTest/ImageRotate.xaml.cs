using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageTest
{
    /// <summary>
    /// ImageRotate.xaml 的交互逻辑
    /// </summary>

    public partial class ImageRotate : Window
    {
        private string LocalPath;
        private string inpath;
        private double maxnum;
        private double currentnum;
        private List<string> list = new List<string>();
        public ImageRotate(string path)
        {
            InitializeComponent();
            inpath = path.Replace("/","\\");
            LocalPath = AppDomain.CurrentDomain.BaseDirectory;
            currentnum = 0;
            GetListString(path);
            maxnum = list.Count;

            Thread th = new Thread(new ThreadStart(SaveImage)) { IsBackground = true };
            th.Start();
        }
        private void GetListString(string path)
        {
            DirectoryInfo thefolder = new DirectoryInfo(path);
            DirectoryInfo[] dirInfo = thefolder.GetDirectories();
            foreach (DirectoryInfo item in dirInfo)
            {
                GetListString(item.FullName);
                string newname = item.FullName.Replace(inpath, LocalPath);
                if (!Directory.Exists(newname))
                {
                    Directory.CreateDirectory(newname);
                }
            }
            FileInfo[] fileinfo = thefolder.GetFiles();
            foreach (FileInfo item1 in fileinfo)
            {
                string value = item1.FullName;
                list.Add(value);
            }
        }
        private void SaveImage()
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (System.IO.Path.GetExtension(list[i]).ToLower() == ".png")
                {
                    InvertColor(list[i]);
                };
            }
        }
        private void InvertColor(string srcFileName)
        {
            Bitmap bitPic = new Bitmap(srcFileName);
            if (!(bitPic.RawFormat.Equals(ImageFormat.Png)))
            {
                MessageBox.Show("Unsuported format,only support for png");
                return;
            }
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bitPic.Width, bitPic.Height);
            var bmpData = bitPic.LockBits(rect, ImageLockMode.ReadWrite, bitPic.PixelFormat); // GDI+ still lies to us - the return format is BGR, NOT RGB.

            IntPtr ptr = bmpData.Scan0;
            // Declare an array to hold the bytes of the bitmap.
            int totalPixels = Math.Abs(bmpData.Stride) * bitPic.Height; //Stride tells us how wide a single line is,width*heith come up with total pixel
            byte[] rgbValues = new byte[totalPixels];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbValues, 0, totalPixels); //RGB=>rgbValus
            if (bitPic.RawFormat.Equals(ImageFormat.Png))
            {
            }
            Marshal.Copy(rgbValues, 0, ptr, totalPixels);
            bitPic.UnlockBits(bmpData);
            bitPic.RotateFlip(RotateFlipType.Rotate180FlipY);

            currentnum++;
            try
            {
                string newfile = srcFileName.Replace(inpath, LocalPath);
                bitPic.Save(newfile);
            }
            catch { }
        }
    }
}
