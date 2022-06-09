using EmployeeAccounting.Model;
using System;
using System.Windows.Input;

namespace EmployeeAccounting.Services
{
    public class ControllCommandWithParameter : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<Employee> execute;

        public ControllCommandWithParameter(Action<Employee> execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute.Invoke((Employee)parameter);
        }
    }
}
