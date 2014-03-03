using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCCSever.RCCAPI
{
    public static class KeyGen
    {
        private static Random _random = new Random();

        /// <summary>
        /// 返回数字字母混合的随机字符串
        /// </summary>
        /// <param name="digit">字符串位数</param>
        /// <returns>生成的随机字符串</returns>
        public static string GetRandomString(int digit)
        {
            int val, index = 0;
            StringBuilder sb = new StringBuilder();
            //阿拉伯数字ASCII值:	49-57
            //大写字母ASCII值:		65-90
            //小写字母ASCII值:		97-122
            do
            {
                val = _random.Next(49, 122);
                //非数字或字母则略过
                if ((val > 57 && val < 65) || (val > 90 && val < 97))
                    continue;
                sb.Append((char)val);
                index++;
            }
            while (index < digit);
            return sb.ToString();
        }
    }
}
