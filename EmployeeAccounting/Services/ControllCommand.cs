using System;
using System.Windows.Input;

namespace EmployeeAccounting.Services
{
    public class ControllCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action execute;

        public ControllCommand(Action execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            execute.Invoke();
        }
    }
}
