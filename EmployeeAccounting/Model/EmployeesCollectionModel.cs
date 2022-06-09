using System.Collections.ObjectModel;

namespace EmployeeAccounting.Model
{
    public class EmployeesCollectionModel
    {
        public static EmployeesCollectionModel employeesCollection = new EmployeesCollectionModel();
        public ObservableCollection<Employee> EmployeesCollection { get; set; }
    }
}
