using EmployeeAccounting.DAL.EntityFramework.Model;
using EmployeeAccounting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAccounting.DAL.EntityFramework.DataAccess
{
    public static class Parser
    {
        public static List<Employee> ToEmployees(List<EmployeeModel> employeeModels)
        {
            var employees = new List<Employee>();
            foreach(var employee in employeeModels)
            {
                employees.Add(new Employee(employee.Id,
                                           employee.FirstName, 
                                           employee.SecondName,
                                           employee.Patronymic,
                                           employee.DateBirth,
                                           employee.Gender,
                                           employee.JobTitle,
                                           employee.SubdivisionName,
                                           employee.DepartamentHeadName));
            }
            return employees;
        }

        public static EmployeeModel ToEmployeeModel(Employee employee)
        {
            var employeeModel = new EmployeeModel();

            employeeModel.Id = employee.Id;
            employeeModel.FirstName = employee.FirstName;
            employeeModel.SecondName = employee.SecondName;
            employeeModel.Patronymic = employee.Patronymic;
            employeeModel.DateBirth = employee.DateBirth;
            employeeModel.Gender = employee.Gender;
            employeeModel.JobTitle = employee.JobTitle;
            employeeModel.SubdivisionName = employee.SubdivisionName;
            employeeModel.DepartamentHeadName = employee.DepartamentHeadName;

            return employeeModel;
        }
    }
}
