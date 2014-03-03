using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows.Controls;

namespace CheckTest
{
    public class WriteControl : ViewModelBase
    {
        public WriteControl()
        {
            delegateLostFocus = CheckTest.ViewModel.MainWindowViewModel.MainModel.ControlValue;
        }
        private TextBox _txtBox;

        public TextBox TxtBox
        {
            get { return _txtBox; }
            set 
            {
                _txtBox = value;
                AddEvent();
            }
        }
        private float _txt;

        public float Txt
        {
            get { return _txt; }
            set{
                RaisePropertyChanging("Txt");
                _txt = value;
                RaisePropertyChanged("Txt");
            }
        }
        public Action<string> delegateLostFocus{get;set;}
        private void AddEvent()
        {
            TxtBox.LostFocus += TxtBox_LostFocus;
        }

        private void TxtBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (delegateLostFocus!=null)
            {
                delegateLostFocus(TxtBox.Name);
            }
        }
    }
}
