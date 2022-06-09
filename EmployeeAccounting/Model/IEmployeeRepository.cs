using System.Collections.Generic;

namespace EmployeeAccounting.Model
{
    public interface IEmployeeRepository 
    {
        public List<Employee> GetAllEmployees();
        public List<string> GetDivisions();
        public void Add(Employee employee);
        public void Delete(Employee employee);
        public void Edit(Employee currentEmployee);
    }
}
