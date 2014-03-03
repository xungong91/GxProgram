using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Test
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            GetListString("D:/Documents/Tencent Files/314568735/Image/");
        }
        private List<string> list = new List<string>();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 30; i++)
            {
                GifImage g = new GifImage();
                g.Source = list[i];

                //string filepath = list[i];
                //byte[] pReadByte = new byte[0];
                //FileStream pFileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                //BinaryReader r = new BinaryReader(pFileStream);

                //r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开
                //pReadByte = r.ReadBytes((int)r.BaseStream.Length);
                //BitmapImage bitmap = new BitmapImage();
                //bitmap.BeginInit();
                //bitmap.StreamSource = new MemoryStream(pReadByte);
                //bitmap.EndInit();
                //g.Source = bitmap;
                stackpanel.Children.Add(g);
            }
                
        }
        private void GetListString(string path)
        {
            DirectoryInfo thefolder = new DirectoryInfo(path);
            DirectoryInfo[] dirInfo = thefolder.GetDirectories();
            foreach (DirectoryInfo item in dirInfo)
            {
                GetListString(item.FullName);
            }
            FileInfo[] fileinfo = thefolder.GetFiles();
            foreach (FileInfo item1 in fileinfo)
            {
                string value = item1.FullName;
                if (value.Trim().ToUpper().EndsWith(".GIF"))
                {
                    list.Add(value);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < stackpanel.Children.Count; i++)
            {
                GifImage g = stackpanel.Children[i] as GifImage;
            }
            stackpanel.Children.Clear();
            GC.Collect();
        }
    }
}
