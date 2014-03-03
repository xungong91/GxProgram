using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
using SeekOauth;
using SeekOauth.Seek;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string Key = "gonggong";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txt2.Text = Dncrypt.EncryptDES(txt1.Text, txt0.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            txt1.Text = Dncrypt.DecryptDES(txt2.Text, txt0.Text);
        }
        private const string path = "http://192.168.98.1/glpt/doLogin.jsp?username=wh001&password=89437";
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string service = "http://192.168.98.1/glpt/Login.jsp";
            string contenttype = "application/x-www-form-urlencoded";//更网站该方法支持的类型要一致
            //根据接口，写参数
            string para = "username=wh001";
            para += "&password=89437";
            //发送请求
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(service);
            myRequest.Method = "POST";
            myRequest.ContentType = contenttype;
            myRequest.ContentLength = para.Length;
            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] postdata = encoding.GetBytes(para);
            newStream.Write(postdata, 0, para.Length);
            newStream.Close();
            // Get response
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();//得到结果
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadData(path);
                byte[] data = client.DownloadData("http://192.168.98.1");
                string htmlstring = Encoding.UTF8.GetString(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //SeekRequest skrequest = new SeekRequest();
            //List<Parameter> plist = new List<Parameter>();
            //plist.Add(new Parameter("username", "wh001"));
            //plist.Add(new Parameter("password", "89437"));
            //string retstr = skrequest.SyncRequest("http://192.168.98.1/glpt/doLogin.jsp", "POST", plist, null);
            //txtmsg.Text = retstr;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            //request.UserAgent = @"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0";
            //Cookie ck = new Cookie("JSESSIONID", "O1ur9Ddn8PJvy+j2IUQPpLfE36Im6It7JwOxM55yZVU=");
            //CookieContainer cc = new CookieContainer();
            //cc.Add(new Uri("http://192.168.98.1"), ck);
            //request.CookieContainer = cc;
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            WebHeaderCollection head = response.Headers;

            string location = head["Location"];
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            //txtmsg.Text = retString;


            myStreamReader.Close();
            myResponseStream.Close();


            string httpstring2 = HttpGet(path, string.Empty);
            try
            {
                WebClient client = new WebClient();
                client.DownloadData(path);
                byte[] data = client.DownloadData("http://192.168.98.1/glpt/menu.jsp");
                string htmlstring = Encoding.UTF8.GetString(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string HttpGet(string url, string queryString)
        {
            string responseData = null;

            if (!string.IsNullOrEmpty(queryString))
            {
                url += "?" + queryString;
            }

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "GET";
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Timeout = 20000;

            StreamReader responseReader = null;

            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
            }
            finally
            {
                webRequest.GetResponse().GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
                webRequest = null;
            }

            return responseData;
        }
    }
    public class Dncrypt
    {
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串 </returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));//转换为字节
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();//实例化数据加密标准
                MemoryStream mStream = new MemoryStream();//实例化内存流
                //将数据流链接到加密转换的流
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
    }
}
