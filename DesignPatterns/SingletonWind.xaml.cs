using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesignPatterns
{
    /// <summary>
    /// SingletonWind.xaml 的交互逻辑
    /// </summary>
    public partial class SingletonWind : Window
    {
        public static SingletonWind Singleton()
        {
            if (_SingletonWind==null)
            {
                _SingletonWind = new SingletonWind();
            }
            return _SingletonWind;
        }
        private static SingletonWind _SingletonWind;
        private SingletonWind()
        {
            InitializeComponent();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            _SingletonWind = null;
            base.OnClosing(e);
        }
    }
}
