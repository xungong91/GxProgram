using System;
using System.Collections.Generic;

using System.Text;

namespace SeekOauth.Seek
{
    public class SeekOauthKey
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        /// <value>The custom key.</value>
        /// <remarks></remarks>
        public string CustomKey { get; set; }
        /// <summary>
        /// 应用secret
        /// </summary>
        /// <value>The custom secret.</value>
        /// <remarks></remarks>
        public string CustomSecret { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        /// <value>The token key.</value>
        /// <remarks></remarks>
        public static string TokenKey { get; set; }
        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <value>The token secret.</value>
        /// <remarks></remarks>
        public string RefreshTokenKey { get; set; }

        

        /// <summary>
        /// Gets or sets the charset.
        /// </summary>
        /// <value>The charset.</value>
        /// <remarks></remarks>
        public Encoding Charset { get; set; }

        public SeekOauthKey()
        {
            CustomKey = null;
            CustomSecret = null;
            TokenKey = null;
            RefreshTokenKey = null;
            Charset = Encoding.UTF8;
        }
    }
}
