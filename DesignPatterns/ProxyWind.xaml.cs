using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace DesignPatterns
{
    /// <summary>
    /// ProxyWind.xaml 的交互逻辑
    /// </summary>
    public partial class ProxyWind : Window
    {
        public ProxyWind()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            wrapPanel.Children.Clear();
            string[] images = HtmlImage.GetHtmlImageUrlList(tb.Text);
            for (int i = 0; i < images.Length; i++)
            {
                HtmlImage.DownImage(images[i]);
            }
        }
    }
    public class HtmlImage
    {
        public static string[] GetHtmlImageUrlList(string sUri)
        {
            WebClient webc = new WebClient();
            byte[] data= webc.DownloadData(sUri);
            string sHtmlText = Encoding.UTF8.GetString(data);

            // 定义正则表达式用来匹配 img 标签
            //Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);    
            Regex regImg = new Regex("(?x)(src|SRC|background|BACKGROUND)=('|\")(http://([\\w-]+\\.)+[\\w-]+(:[0-9]+)*(/[\\w-]+)*(/[\\w-]+\\.(jpg|JPG|png|PNG|gif|GIF)))('|\")"); 
            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(sHtmlText);

            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = getimageurl(match.Value);

            return sUrlList;
        }
        public static string getimageurl(string img)
        {
            string result = img.Substring(5);
            result = result.Substring(0, result.Length - 1);
            return result;
        }
        public static void DownImage(string imageUri)
        {
            string filename=imageUri.Substring(imageUri.LastIndexOf(@"/")+1);
            string filepath = AppDomain.CurrentDomain.BaseDirectory +"images\\"+DateTime.Now.Millisecond.ToString()+ filename;
            WebClient mywebclient = new WebClient();
            mywebclient.DownloadFile(imageUri, filepath);

            string aSize;
            string aLength;
            return;
            try
            {
                Uri mUri = new Uri(imageUri);
                HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create(mUri);
                mRequest.Method = "GET";
                mRequest.Timeout = 200;
                mRequest.ContentType = "text/html;charset=utf-8";
                HttpWebResponse mResponse = (HttpWebResponse)mRequest.GetResponse();
                Stream mStream = mResponse.GetResponseStream();
                aSize = (mResponse.ContentLength / 1024).ToString() + "KB";

                mStream.Close();
            }
            catch (Exception e)
            {
                //MessageBox.Show(aPhotoUrl + "获取失败");  
            }  
        }
    }
}
