using System;
using System.Windows.Input;

namespace ConwaysGameOfLife.Infrastructure
{
    /// <summary>
    /// Represents a command that can be executed.
    /// </summary>
    /// <typeparam name="T">Type of the command parameter.</typeparam>
    /// <remarks>Implementation taken from the web. Googled for ICommand implementation.</remarks>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute = null;
        private readonly Predicate<T> _canExecute = null;
       
        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }
        
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
