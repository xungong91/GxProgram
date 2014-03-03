using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CheckTest.Model;
using CheckTest.ViewModel;

namespace CheckTest.View
{
    /// <summary>
    /// CalculateView.xaml 的交互逻辑
    /// </summary>
    public partial class CalculateView 
    {
        private static CalculateView _CalculateView;
        public static CalculateView Singleton()
        {
            if (_CalculateView==null)
            {
                _CalculateView = new CalculateView();
            }
            return _CalculateView;
        }
        private CalculateView()
        {
            InitializeComponent();
            listbox.SetBinding(ListBox.ItemsSourceProperty, new Binding() { Source = MainWindowViewModel.MainModel.CalculateList });
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            _CalculateView = null;
            base.OnClosing(e);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtbox.Text)) return;
            FormulaModel model = new FormulaModel() { Txt = txtbox.Text, IsPass = true };
            MainWindowViewModel.MainModel.CalculateList.Add(model);
        }

        private void listbox_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (listbox.SelectedItem != null)
            {
                try
                {
                    MainWindowViewModel.MainModel.CalculateList.Remove(listbox.SelectedItem as FormulaModel);
                }
                catch { }
            }
        }
    }
}
