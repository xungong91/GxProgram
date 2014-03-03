using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CheckTest.Model
{
    public class FormulaModel:ViewModelBase
    {
        public FormulaModel()
        {
        }

        private string _txt;

        public string Txt
        {
            get { return _txt; }
            set
            {
                RaisePropertyChanging("Txt");
                _txt = value;
                RaisePropertyChanged("Txt");
            }
        }
        private bool _isPass;

        public bool IsPass
        {
            get { return _isPass; }
            set
            {
                RaisePropertyChanging("IsPass");
                _isPass = value;
                RaisePropertyChanged("IsPass");
            }
        }
    }
}
