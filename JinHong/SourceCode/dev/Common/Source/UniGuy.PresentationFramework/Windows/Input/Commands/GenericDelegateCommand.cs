using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace UniGuy.Commands
{
    /// <summary>
    /// 泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateCommand<T> : ICommand where T : class
    {

        private readonly Action<T> execute;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action<T> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            this.execute(parameter as T);
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute();
        }
    }
}
