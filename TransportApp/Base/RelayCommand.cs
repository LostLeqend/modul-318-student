using System;
using System.Windows.Input;

namespace TransportApp.Base
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> methodToExecute)
            : this(methodToExecute, null)
        {
        }

        public RelayCommand(Action<object> methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this._methodToExecute = methodToExecute;
            this._canExecuteEvaluator = canExecuteEvaluator;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Action<object> _methodToExecute;
        private readonly Func<bool> _canExecuteEvaluator;

        public bool CanExecute(object parameter)
        {
            if (this._canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = this._canExecuteEvaluator.Invoke();
                return result;
            }
        }

        public void Execute(object parameter)
        {
            this._methodToExecute.Invoke(parameter);
        }
    }
}
