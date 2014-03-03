using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MetroMvvm.Helpers;

namespace MetroMvvm.Models
{
    public class Person:NotificationObject
    {
        public Person(string name, int age, bool isMarried, bool isRead, DateTime readDate, Color favColor)
        {
            Name = name;
            Age = age;
            IsMarried = isMarried;
            IsRead = isRead;
            ReadDate = readDate;
            FavoriteColor = new SolidColorBrush(favColor);
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set 
            {
                if (_name!=value)
                {
                    _name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        private int _age;

        public int Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    RaisePropertyChanged(() => Age);
                }
            }
        }

        private bool _isMarried;

        public bool IsMarried
        {
            get { return _isMarried; }
            set
            {
                if (_isMarried != value)
                {
                    _isMarried = value;
                    RaisePropertyChanged(() => IsMarried);
                }
            }
        }

        private bool _isRead;

        public bool IsRead
        {
            get { return _isRead; }
            set
            {
                if (_isRead != value)
                {
                    _isRead = value;
                    RaisePropertyChanged(() => IsRead);
                }
            }
        }

        private DateTime _readDate;
        public DateTime ReadDate
        {
            get { return _readDate; }
            set
            {
                if (_readDate != value)
                {
                    _readDate = value;
                    RaisePropertyChanged(() => ReadDate);
                }
            }
        }

        private SolidColorBrush _favoriteColor;
        public SolidColorBrush FavoriteColor
        {
            get { return _favoriteColor; }
            set
            {
                if (_favoriteColor != value)
                {
                    _favoriteColor = value;
                    RaisePropertyChanged(() => FavoriteColor);
                }
            }
        }

        private Exam _exam;

        public Exam Exam
        {
            get { return _exam; }
            set 
            {
                if (_exam != value)
                {
                    _exam = value;
                    RaisePropertyChanged(() => Exam);
                }
            }
        }
    }
}
