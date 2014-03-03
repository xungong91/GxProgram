using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageTest
{
    /// <summary>
    /// ImageGif.xaml 的交互逻辑
    /// </summary>
    public partial class ImageGif : Window
    {
        public ImageGif()
        {
            InitializeComponent();
        }
        public void GetPngs(string file)
        {
            file = file.Replace("/", "\\");
            string directory = Path.GetDirectoryName(file);

            Image imgGif = Image.FromFile(file);
            FrameDimension ImgFrmDim = new FrameDimension(imgGif.FrameDimensionsList[0]);
            int nFrameCount = imgGif.GetFrameCount(ImgFrmDim);
            for (int i = 0; i < nFrameCount; i++)
            {
                imgGif.SelectActiveFrame(ImgFrmDim, i);
                imgGif.Save(string.Format("{0}\\{1}.png",directory, i), ImageFormat.Png);
            }
        }
    }
}
