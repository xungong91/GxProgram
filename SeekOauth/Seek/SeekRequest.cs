using System;
using System.Collections.Generic;

using System.Text;

namespace SeekOauth.Seek
{
    public class SeekRequest
    {
        //同步http请求
        public string SyncRequest(string url, string httpMethod, List<Parameter> listParam, List<Parameter> listFile)
        {
            StringBuilder sbqueryString = new StringBuilder();
            string queryString=null;
            foreach (Parameter pa in listParam)
            {
                /////////2013年3月18日17:47:04修改为urlEncoding编码
                sbqueryString.AppendFormat("{0}={1}&", pa.Name, UrlEncode(pa.Value));
            }
            if (sbqueryString.Length > 0)
                queryString = sbqueryString.ToString().Substring(0, sbqueryString.Length - 1);

            string oauthUrl = url;
            SyncHttp http = new SyncHttp();
            if (httpMethod == "GET")
            {
                return http.HttpGet(oauthUrl, queryString);
            }
            else if ((listFile == null) || (listFile.Count == 0))
            {
                return http.HttpPost(oauthUrl, queryString);
            }
            else
            {
                return http.HttpPostWithFile(oauthUrl, queryString, listFile);
            }
        }

        //转码为UTF-8
        public string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }
    }
}
