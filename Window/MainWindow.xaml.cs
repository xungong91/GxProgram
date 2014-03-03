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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            int int1= Convert.ToInt32("b4", 16);
            int int2 = Convert.ToInt32("00", 16);
            int int3 = Convert.ToInt32("ff", 16);
        }
        private void DarkButtonClick(object sender, RoutedEventArgs e)
        {
        }

        private void LightButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void BlueButtonClick(object sender, RoutedEventArgs e)
        {
            new Window2().Show();
        }

        private void RedButtonClick(object sender, RoutedEventArgs e)
        {
            Color c = Color.FromRgb(180, 0, 255);
            this.Resources["AccentColor"] = c;
        }

        private void GreenButtonClick(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1();
            w1.Show();
        }
    }
}
