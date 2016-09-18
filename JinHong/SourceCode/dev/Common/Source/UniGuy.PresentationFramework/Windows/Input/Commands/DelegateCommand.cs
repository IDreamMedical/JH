using System;
using System.Windows.Input;

namespace UniGuy.Commands
{
    public sealed class DelegateCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

       



        public void Execute(object parameter)
        {
            this.execute();
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute();
        }
    }
}
