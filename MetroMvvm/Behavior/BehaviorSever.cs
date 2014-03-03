using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetroMvvm.Behavior
{
    public static class BehaviorSever
    {
        public static void SetOpenFlyoutBehavior(object obj, OpenFlyoutBehavior b)
        {
            b.Attach(obj);
        }
        public static void SetCloseFlyoutBehavior(object obj, CloseFlyoutBehavior b)
        {
            b.Attach(obj);
        }
        public static void SetDivorceBehavior(object obj, DivorceBehavior b)
        {
            b.Attach(obj);
        }
        public static void SetEnterCreditWind(object obj, EnterCreditWind b)
        {
            b.Attach(obj);
        }
        public static void SetDataGridSelectedCellsChanged(object obj, DataGridSelectedCellsChanged b)
        {
            b.Attach(obj);
        }
    }
}
