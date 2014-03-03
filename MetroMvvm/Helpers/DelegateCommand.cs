using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MetroMvvm.Helpers
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _command;
        private readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action command, Func<bool> canExecute = null)
        {
            if (command == null)
                throw new ArgumentNullException();
            _canExecute = canExecute;
            _command = command;
        }

        private readonly Func<object,bool> _command2;
        public DelegateCommand(Func<object, bool> command)
        {
            if (command == null)
                throw new ArgumentNullException();
            _canExecute = null;
            _command = null;
            _command2 = command;
        }

        public void Execute(object parameter)
        {
            if (_command != null)
            {
                _command();
            }
            else
            {
                _command2(parameter);
            }

        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

    }
}
