using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MetroMvvm.Models;

namespace MetroMvvm.Behavior
{
    public class OpenFlyoutBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += AssociatedObject_Click;
        }
        void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button b = sender as Button;
            Flyout fl = b.Tag as Flyout;
            if (fl.Tag == null)
                fl.IsOpen = !fl.IsOpen;
            else if ((fl.Tag is Button) && ((fl.Tag as Button) == b))
                fl.IsOpen = !fl.IsOpen;
            else if ((fl.Tag is Button) && ((fl.Tag as Button) != b))
                fl.IsOpen = true;

            fl.Tag = b;
            fl.DataContext = b.DataContext;
        }
    }
    public class CloseFlyoutBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += AssociatedObject_Click;
        }
        void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button b = sender as Button;
            Flyout fl = b.Tag as Flyout;
            fl.IsOpen = false;
        }
    }
    public class DivorceBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += AssociatedObject_Click;
        }
        void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button b = sender as Button;
            Person p = b.DataContext as Person;
            p.IsMarried = false;
        }
    }
    public class DataGridSelectedCellsChanged : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectedCellsChanged += AssociatedObject_Click;
        }
        void AssociatedObject_Click(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid b = sender as DataGrid;
            Flyout f = b.Tag as Flyout;

            Person p;
            if (b.SelectedIndex != -1)
            {
                p = (Person)b.SelectedItem;
                if (f.IsOpen)
                    f.DataContext = p;
            }

        }
    }
}
