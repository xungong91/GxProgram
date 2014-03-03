using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CheckTest
{
        [ValueConversion(typeof(string), typeof(string))]
        public class JoMoneyConvert : IValueConverter
        {
            #region IValueConverter 成员

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                string moneyvalue = "";
                try
                {
                    moneyvalue = value.ToString();
                    decimal mnval = decimal.Parse(moneyvalue);
                    moneyvalue = string.Format("{0:N2}", mnval);
                }
                catch
                {
                    return "0.00";
                }
                return moneyvalue;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return value;
            }

            #endregion
        }

        [ValueConversion(typeof(string), typeof(string))]
        public class JoShortDateConvert : IValueConverter
        {

            #region IValueConverter 成员

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                //string datestr = value.ToString();

                string retstr;
                try
                {
                    string datestr = value.ToString().Replace("-", "");
                    retstr = string.Format("{0}年{1}月{2}日", datestr.Substring(0, 4), datestr.Substring(4, 2), datestr.Substring(6, 2));
                }
                catch
                {
                    return "1900年01月01日";
                }
                return retstr;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return value;
            }

            #endregion
        }
}
