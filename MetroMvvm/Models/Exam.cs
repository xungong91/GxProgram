using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetroMvvm.Helpers;

namespace MetroMvvm.Models
{
    public class Exam:NotificationObject
    {
        public Exam(double e1, double e2, double e3, double e4, double e5, double e6)
        {
            Exam1 = e1;
            Exam2 = e2;
            Exam3 = e3;
            Exam4 = e4;
            Exam5 = e5;
            Exam6 = e6;
        }


        private double _exam1;

        public double Exam1
        {
            get { return _exam1; }
            set
            {
                if (_exam1!=value)
                {
                    _exam1 = value;
                    RaisePropertyChanged(()=>Exam1);
                }
            }
        }

        private double _exam2;

        public double Exam2
        {
            get { return _exam2; }
            set
            {
                if (_exam2 != value)
                {
                    _exam2 = value;
                    RaisePropertyChanged(() => Exam2);
                }
            }
        }

        private double _exam3;

        public double Exam3
        {
            get { return _exam3; }
            set
            {
                if (_exam3 != value)
                {
                    _exam3 = value;
                    RaisePropertyChanged(() => Exam3);
                }
            }
        }

        private double _exam4;

        public double Exam4
        {
            get { return _exam4; }
            set
            {
                if (_exam4 != value)
                {
                    _exam4 = value;
                    RaisePropertyChanged(() => Exam4);
                }
            }
        }

        private double _exam5;

        public double Exam5
        {
            get { return _exam5; }
            set
            {
                if (_exam5 != value)
                {
                    _exam5 = value;
                    RaisePropertyChanged(() => Exam5);
                }
            }
        }

        private double _exam6;

        public double Exam6
        {
            get { return _exam6; }
            set
            {
                if (_exam6 != value)
                {
                    _exam6 = value;
                    RaisePropertyChanged(() => Exam6);
                }
            }
        }
    }
}
