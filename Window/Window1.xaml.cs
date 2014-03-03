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
using System.Windows.Shapes;
using System.Windows.Interop;
using Test.Native;
using System.IO;

namespace Test
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1
    {
        public Window1()
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tb.Text=GetIP();
        }
        private string GetIP()
        {
            Uri uri = new Uri("http://www.ikaka.com/ip/index.asp");
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = 0;
            req.CookieContainer = new System.Net.CookieContainer();
            req.GetRequestStream().Write(new byte[0], 0, 0);
            System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)(req.GetResponse());
            StreamReader rs = new StreamReader(res.GetResponseStream(), System.Text.Encoding.GetEncoding("GB18030"));
            string s = rs.ReadToEnd();
            rs.Close();
            req.Abort();
            res.Close();
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(s, @"\d+.\d+.\d+.\d+");
            if (m.Success) return m.Groups[0].Value;
            return string.Empty;
        }
    }
}
