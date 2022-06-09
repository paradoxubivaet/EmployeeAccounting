using EmployeeAccounting.DAL.EntityFramework.Model;
using EmployeeAccounting.Messages;
using EmployeeAccounting.Model;
using EmployeeAccounting.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EmployeeAccounting.DAL.EntityFramework.DataAccess
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public List<Employee> GetAllEmployees()
        {
            using (AccountingContext context = new AccountingContext())
            {
                List<Employee> employees = Parser.ToEmployees(context.EmployeeAccounts.ToList());
                return employees;
            }
        }

        public void Add(Employee employee)
        {
            using (AccountingContext context = new AccountingContext())
            {
                var employeeModel = Parser.ToEmployeeModel(employee);
                context.EmployeeAccounts.Add(employeeModel);
                context.SaveChanges();
                UpdateLocalStore();
            }
        }

        public void Edit(Employee currentEmployee)
        {
            using (AccountingContext context = new AccountingContext())
            {
                var currentEmployeeModel = Parser.ToEmployeeModel(currentEmployee);
                var editingEmployeeModel = context.EmployeeAccounts.
                                                   Where(x => x.Id == currentEmployeeModel.Id).
                                                   FirstOrDefault();

                editingEmployeeModel.FirstName = currentEmployeeModel.FirstName;
                editingEmployeeModel.SecondName = currentEmployeeModel.SecondName;
                editingEmployeeModel.Patronymic = currentEmployeeModel.Patronymic;
                editingEmployeeModel.DateBirth = currentEmployeeModel.DateBirth;
                editingEmployeeModel.JobTitle = currentEmployeeModel.JobTitle;
                editingEmployeeModel.DepartamentHeadName = currentEmployeeModel.DepartamentHeadName;
                editingEmployeeModel.SubdivisionName = currentEmployeeModel.SubdivisionName;
                editingEmployeeModel.Gender = currentEmployeeModel.Gender;

                context.SaveChanges();
                UpdateLocalStore();
            }
        }

        public void Delete(Employee employee)
        {
            using (AccountingContext context = new AccountingContext()) 
            {
                var deletingEmployee = context.EmployeeAccounts.Where(x => x.Id == employee.Id)
                                                               .FirstOrDefault();
                context.EmployeeAccounts.Remove(deletingEmployee);
                context.SaveChanges();
                UpdateLocalStore();
            }
        }

        public List<string> GetDivisions()
        {
            using(AccountingContext context = new AccountingContext())
            {
                List<string> divisions = new List<string>();

                foreach (EmployeeModel employee in context.EmployeeAccounts)
                {
                    var division = employee.SubdivisionName;

                    if (division != null || division != "")
                    {
                        if (!divisions.Contains(division))
                            divisions.Add(employee.SubdivisionName);
                    }
                }

                divisions.Add("");
                return divisions;
            }
        }

        private void UpdateLocalStore()
        {
            using(AccountingContext context = new AccountingContext())
            {
                List<Employee> employees = Parser.ToEmployees(context.EmployeeAccounts.ToList());
                EmployeesCollectionModel.employeesCollection.EmployeesCollection = new ObservableCollection<Employee>(employees);

                var messegner = Ioc.Default.GetService<IMessenger>();
                messegner.Send(new UpdateMessage(true));
            }
        }
    }
}
