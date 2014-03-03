﻿using System;
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
using CheckTest.ViewModel;

namespace CheckTest.View
{
    /// <summary>
    /// TableMainView.xaml 的交互逻辑
    /// </summary>
    public partial class TableMainView
    {
        TableMainViewModel model=new TableMainViewModel();
        public TableMainView()
        {
            InitializeComponent();
            this.DataContext = model;
        }

        private void MetroWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
        }
    }
}
