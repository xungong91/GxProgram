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
using CheckTest.ViewModel;

namespace CheckTest.View
{
    /// <summary>
    /// UserTable1.xaml 的交互逻辑
    /// </summary>
    public partial class UserTable1 : UserControl
    {
        public UserTable1()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            UserTable1Model model = this.DataContext as UserTable1Model;
            model.C1.TxtBox = C1;
            model.C2.TxtBox = C2;
            model.C3.TxtBox = C3;
            model.C4.TxtBox = C4;
            model.C5.TxtBox = C5;
            model.C6.TxtBox = C6;
            model.C7.TxtBox = C7;
            model.C8.TxtBox = C8;
            model.C9.TxtBox = C9;
            model.C10.TxtBox = C10;
            model.C11.TxtBox = C11;
            model.C12.TxtBox = C12;
            model.C13.TxtBox = C13;
            model.C14.TxtBox = C14;
            model.C15.TxtBox = C15;
            model.C16.TxtBox = C16;
            model.C17.TxtBox = C17;
            model.C18.TxtBox = C18;
            //model.C19.TxtBox = C19;
            model.C20.TxtBox = C20;
            model.C21.TxtBox = C21;
            model.C22.TxtBox = C22;
            model.C23.TxtBox = C23;
            model.C24.TxtBox = C24;
            model.C25.TxtBox = C25;
            model.C26.TxtBox = C26;
            model.C27.TxtBox = C27;
            model.C28.TxtBox = C28;
            model.C29.TxtBox = C29;
            model.C30.TxtBox = C30;
            model.C31.TxtBox = C31;
            model.C32.TxtBox = C32;
            model.C33.TxtBox = C33;
            model.C34.TxtBox = C34;
            model.C35.TxtBox = C35;
            model.C36.TxtBox = C36;
        }
    }
}
