using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using MetroMvvm.Models;
using MetroMvvm.ViewModels;
using MetroMvvm.Views;

namespace MetroMvvm.Behavior
{
    public class EnterCreditWind : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += AssociatedObject_Click;
        }
        void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button b = sender as Button;
            WindCredit wc = new WindCredit();
            wc.Show();
            (wc.Resources["WindCreditViewModel"] as WindCreditViewModel).Person = (sender as Button).DataContext as Person;
        }
    }
}
