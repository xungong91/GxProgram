using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;

namespace Test.Behavior
{
    public static class BehaviorSever
    {
        public static void SetReturnKeyBehavior(object obj, ReturnKeyBehavior b)
        {
            b.Attach(obj);
        }
    }
}
