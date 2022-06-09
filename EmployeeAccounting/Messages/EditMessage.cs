using EmployeeAccounting.Model;

namespace EmployeeAccounting.Messages
{
    public class EditMessage
    {
        public Employee Employee { get; }
        public EditMessage(ref Employee employee)
        {
            Employee = employee;
        }
    }
}
