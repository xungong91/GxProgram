using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using MetroMvvm.Helpers;
using MetroMvvm.Models;

namespace MetroMvvm.ViewModels
{
    class MainWindowViewModel:BaseViewModel
    {
        public MainWindowViewModel()
        {
            RandomizeData();
        }
        private void RandomizeData()
        {

            PersonsCollection = new ObservableCollection<Person>();

            for (var i = 0; i < 5; i++)
            {
                PersonsCollection.Add(new Person(
                    RandomHelper.RandomString(10, true),
                    RandomHelper.RandomInt(1, 43),
                    RandomHelper.RandomBool(),
                     RandomHelper.RandomBool(),
                    RandomHelper.RandomDate(new DateTime(1980, 1, 1), DateTime.Now),
                    RandomHelper.RandomColor()
                    ));
            }
            
        }
        private ObservableCollection<Person> _personsCollection;

        public ObservableCollection<Person> PersonsCollection
        {
            get { return _personsCollection; }
            set 
            {
                if (_personsCollection!=value)
                {
                    _personsCollection = value;
                }
            }
        }
        public ICommand Commandadd
        {
            get { return new DelegateCommand(onCommandadd); }
        }
        private void onCommandadd()
        {
            PersonsCollection.Add(new Person(
                    RandomHelper.RandomString(10, true),
                    RandomHelper.RandomInt(1, 43),
                    RandomHelper.RandomBool(),
                     RandomHelper.RandomBool(),
                    RandomHelper.RandomDate(new DateTime(1980, 1, 1), DateTime.Now),
                    RandomHelper.RandomColor()
                    ));
        }
    }


    public class BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool b = System.Convert.ToBoolean(value);
            if(b)
                return "是";
            else
                return "否";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string strb = value.ToString();
            bool b=true;
            switch (strb)
            {
                case "是":
                    b = true;
                    break;
                case "否":
                    b = false;
                    break;
            }
            return b;
        }
    }
}
