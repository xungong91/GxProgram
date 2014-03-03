using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTest.Model
{
    public class SingleDropList
    {
        public List<string> List { get; set; }
        public SingleDropList()
        {
            List = new List<string>()
            {
                "星期一","星期二","星期三","星期四","星期五","星期六"
            };
        }
    }
}
