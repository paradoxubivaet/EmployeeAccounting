using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using EmployeeAccounting.DAL.EntityFramework.DataAccess;
using EmployeeAccounting.Model;
using EmployeeAccounting.Services;
using EmployeeAccounting.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace EmployeeAccounting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(new ServiceCollection()
                .AddSingleton<MainWindowViewModel>()
                .AddTransient<CreateAndEditWindowViewModel>()
                .AddTransient<FilterWindowViewModel>()
                .AddSingleton<IWindowService, WindowService>()
                .AddSingleton<IMessenger, Messenger>()
                .AddTransient<IEmployeeRepository, EmployeeRepository>()
                .BuildServiceProvider());

            LoadData();
        }

        public void LoadData()
        {
            using(AccountingContext context = new AccountingContext())
            {
                List<Employee> employees = Parser.ToEmployees(context.EmployeeAccounts.ToList());
                EmployeesCollectionModel.employeesCollection.EmployeesCollection = new ObservableCollection<Employee>(employees);
            }
        }
    }
}
