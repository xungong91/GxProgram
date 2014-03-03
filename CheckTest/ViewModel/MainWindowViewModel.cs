using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using CheckTest.View;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Reflection;
using Microsoft.Scripting;
using CheckTest.Model;
using CheckTest.Control;
using System.Text.RegularExpressions;

namespace CheckTest.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel MainModel;
        public RelayCommand CommandSubmit { get; set; }
        public RelayCommand CommandFormula { get; set; }
        public RelayCommand CommandCalculate { get; set; }
        public MainWindowViewModel() 
        {
            Pensen p = new Pensen();
            MainModel = this;
            Table1 = new UserTable1();
            FormulaList = new System.Collections.ObjectModel.ObservableCollection<FormulaModel>();
            CalculateList = new System.Collections.ObjectModel.ObservableCollection<FormulaModel>();
            Table1.DataContext = Table1model = new UserTable1Model();
            CommandSubmit = new RelayCommand(
                () =>
                {
                    ReflectionTest();
                });
            CommandFormula = new RelayCommand(
                () =>
                {
                    FormulaView.Singleton().Show();
                }
                );
            CommandCalculate = new RelayCommand(
                () =>
                {
                    CalculateView.Singleton().Show();
                }
                );
        }
        private UserControl _table1;
        public UserTable1Model Table1model { get; set; }
        public UserControl Table1
        {
            get { return _table1; }
            set 
            {
                RaisePropertyChanging("Table1");
                _table1 = value;
                RaisePropertyChanged("Table1");
            }
        }
        private string _txtCondition;

        public string TxtCondition
        {
            get { return _txtCondition; }
            set { _txtCondition = value; }
        }
        /// <summary>
        /// 审核公式
        /// </summary>
        public System.Collections.ObjectModel.ObservableCollection<FormulaModel> FormulaList { get; set; }
        /// <summary>
        /// 计算公式
        /// </summary>
        public System.Collections.ObjectModel.ObservableCollection<FormulaModel> CalculateList { get; set; }

        private bool StartScript()
        {
            PythonHelper py = new PythonHelper();
            py.ExeCalculate(Table1model, CalculateList);
            return py.ExeFormula(Table1model,FormulaList);
        }
        private void ReflectionTest()
        {
            string result = TxtCondition;
            if (StartScript())
            {
                global::System.Windows.MessageBox.Show("通过");
            }
            else
            {
                global::System.Windows.MessageBox.Show("没通过验证");
                FormulaView.Singleton().Show();
            }
        }
        public void ControlValue(string felem)
        {
            Regex r = new Regex("^.+(" + felem + ")$|^.+(" + felem + ")\\D{1}\\w+");
            Match m;
            for (int i = 0; i < CalculateList.Count; i++)
            {
                m = r.Match("C1=C2+C3");
                if (m.Success)
                {

                };
            }
        }
    }
}
