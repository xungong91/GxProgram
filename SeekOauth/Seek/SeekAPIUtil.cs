using System;
using System.Collections.Generic;

using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace SeekOauth.Seek
{
    public class SeekAPIUtil
    {
        public bool access_token(SeekOauthKey key, string oauthip)
        {
            string url = "http://" + oauthip + "/oauth/access_token";
            List<Parameter> pars = new List<Parameter>();
            pars.Add(new Parameter("grant_type", "none"));
            pars.Add(new Parameter("client_id", key.CustomKey));
            pars.Add(new Parameter("client_secret", key.CustomSecret));
            SeekRequest request=new SeekRequest();
            string requeststring = request.SyncRequest(url, "POST", pars, null);
            JsonReader reader = new JsonTextReader(new StringReader(requeststring));
            //string msg = "";
            while (reader.Read())
            {
                if (reader.Path.Equals("access_token") && reader.TokenType.ToString().Equals("String"))
                    SeekOauthKey.TokenKey = reader.Value.ToString();
                else if (reader.Path.Equals("refresh_token") && reader.TokenType.ToString().Equals("String"))
                    key.RefreshTokenKey = reader.Value.ToString();
                //msg += "TokenType:" + reader.TokenType + "|ValueType:" + reader.ValueType.Name + "|Value:" + reader.Value + "|Path:" + reader.Path + "\n";
            }
           //// SeekOauthKey.TokenKey = msg;
            //SeekOauthKey.TokenKey = GetJsonValue(requeststring, "access_token");
            //key.RefreshTokenKey = GetJsonValue(requeststring, "refresh_token");
            if (SeekOauthKey.TokenKey == null) return false;
            return true;
        }

        public string get_nsrxx(SeekOauthKey key)
        {
            string url = "http://192.168.1.247/crmrest/crm.api/nsrxx/420104629007840/json";
            List<Parameter> pars = new List<Parameter>();
            pars.Add(new Parameter("oauth_token", SeekOauthKey.TokenKey));
            SeekRequest request = new SeekRequest();
            string requeststring = request.SyncRequest(url, "GET", pars, null);
            return requeststring;
        }

        public bool grant_type(SeekOauthKey key)
        {
            string url = "http://192.168.1.202/oauth/access_token";
            List<Parameter> pars = new List<Parameter>();
            pars.Add(new Parameter("grant_type", "refresh_token"));
            pars.Add(new Parameter("client_id", key.CustomKey));
            pars.Add(new Parameter("client_secret", key.CustomSecret));
            pars.Add(new Parameter("refresh_token", key.RefreshTokenKey));
            SeekRequest request = new SeekRequest();
            string requeststring = request.SyncRequest(url, "POST", pars, null);
            SeekOauthKey.TokenKey = GetJsonValue(requeststring, "access_token");
            key.RefreshTokenKey = GetJsonValue(requeststring, "refresh_token");
            if (SeekOauthKey.TokenKey == null) return false;
            return true;
        }

        public string GetJsonValue(string jsonstring, string name)
        {
            jsonstring = jsonstring.Replace("{", "").Replace("}", "");
            string[] substring = jsonstring.Split(new char[] { ',' });
            string ret = "";
            for (int i = 0; i < substring.Length; i++)
            {
                if (substring[i].Contains("\"" + name + "\""))
                {
                    ret = substring[i].Substring(name.Length + 3, substring[i].Length - name.Length - 3);
                    if (ret[0].ToString().Equals("\""))
                        ret = ret.Replace("\"", "");
                    break;
                }
            }
            return ret;
        }

    }
}
