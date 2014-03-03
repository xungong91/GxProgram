using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MetroMvvm.Models;
using MetroMvvm.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MetroMvvm.ViewModels
{
    class WindCreditViewModel:BaseViewModel
    {
        public WindCreditViewModel()
        {
            RandomizeData();
        }
        private void RandomizeData()
        {
            Exam = new Exam(
                RandomHelper.RandomDouble() * 100, 
                RandomHelper.RandomDouble() * 100, 
                RandomHelper.RandomDouble() * 100, 
                RandomHelper.RandomDouble() * 100, 
                RandomHelper.RandomDouble() * 100, 
                RandomHelper.RandomDouble() * 100);
        }
        private Exam _exam;
        public Exam Exam
        {
            get { return _exam; }
            set 
            {
                if (_exam!=value)
                {
                    _exam = value;
                    RaisePropertyChanged(() => Exam);
                }
            }
        }
        private Person _person;

        public Person Person
        {
            get { return _person; }
            set
            {
                if (_person!=value)
                {
                    _person = value;
                    if (_person.Exam != null)
                        Exam = _person.Exam;
                    else
                        _person.Exam = Exam;
                    RaisePropertyChanged(() => Person);
                }
            }
        }
        private int _role;

        public int Role
        {
            get { return _role; }
            set 
            {
                if (_role!=value)
                {
                    _role = value;
                    RaisePropertyChanged(() => Role);
                }
            }
        }



        public ICommand CloseWind 
        { 
            get
            {
                return new DelegateCommand(OnCloseWind);
            } 
        }

        private bool OnCloseWind(object w)
        {
            Window.GetWindow(w as Button).Close();
            return true;
        }
    }

    public class CreadRule: ValidationRule
    {
        public string Rule { get; set; }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double d;
            if (double.TryParse(value.ToString(), out d))
            {
                if (d >= 20)
                    return new ValidationResult(true, null);
                else
                    return new ValidationResult(false, "不符合规则：" + Rule);
            }
            return new ValidationResult(false, "Illegal characters or ");
        }
    }
}
