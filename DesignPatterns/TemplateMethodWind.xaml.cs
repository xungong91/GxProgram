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
    /// TemplateMethodWind.xaml 的交互逻辑
    /// </summary>
    public partial class TemplateMethodWind : Window
    {
        public TemplateMethodWind()
        {
            InitializeComponent();
        }
        public static string UserName;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserName = txtb.Text;
            ShowTemplateMsg msg = new ShowConcreteMsg();
            msg.ShowMsg();
        }
    }
    public abstract class ShowTemplateMsg
    {
        public abstract string MsgString();
        public void ShowMsg()
        {
            string msg = string.Format("欢迎你，{0}", MsgString());
            MessageBox.Show(msg);
        }
    }
    public class ShowConcreteMsg:ShowTemplateMsg
    {
        public override string MsgString()
        {
            return TemplateMethodWind.UserName;
        }
    }
}
