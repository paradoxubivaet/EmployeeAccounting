using EmployeeAccounting.Messages;
using EmployeeAccounting.Model;
using EmployeeAccounting.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeAccounting.ViewModel
{
    public class CreateAndEditWindowViewModel : INotifyPropertyChanged
    {
        private EmployeesCollectionModel employeesCollectionModel;
        private Employee currentEmployee;
        private int windowId;
        private bool flagNewEmployee;

        private string firstName;
        private string secondName;
        private string patronymic;
        private DateTime dateBirth;
        private string gender;
        private string jobTitle;
        private string? subdivisionName;
        private string? departamentHeadName;

        private IMessenger messenger;
        private IEmployeeRepository repository;
        private IWindowService windowService;

        public ControllCommand InvokeSaveCommand { get; private set; }
        public ControllCommand InvokeClearFieldCommand { get; private set; }
        public ControllCommand InvokePromoteCommand { get; private set; }
        public ControllCommand InvokeDemoteCommand { get; private set; }
        public ControllCommand MinimizeWindowCommand { get; private set; }
        public ControllCommand MaximizeWindowCommand { get; private set; }
        public ControllCommand CloseWindowCommand { get; private set; }

        public CreateAndEditWindowViewModel(IMessenger messenger,
                                            IEmployeeRepository repository,
                                            IWindowService windowService)
        {
            employeesCollectionModel = EmployeesCollectionModel.employeesCollection;

            this.messenger = messenger;
            this.repository = repository;
            this.windowService = windowService;

            messenger.Subscribe<EditMessage>(this, GetField);
            messenger.Subscribe<GetIdMessage>(this, GetId);

            InvokeSaveCommand = new ControllCommand(SaveEmployee);
            InvokeClearFieldCommand = new ControllCommand(ClearField);
            InvokePromoteCommand = new ControllCommand(PromoteEmployee);
            InvokeDemoteCommand = new ControllCommand(DemoteEmployee);

            MinimizeWindowCommand = new ControllCommand(MinimizeWindow);
            MaximizeWindowCommand = new ControllCommand(MaximizeWindow);
            CloseWindowCommand = new ControllCommand(CloseWindow);

            windowId = -1;
            flagNewEmployee = true;

            ClearField();
        }

        public ObservableCollection<Employee> EmployeesCollection
        {
            get
            {
                return employeesCollectionModel.EmployeesCollection;
            }
            set
            {
                employeesCollectionModel.EmployeesCollection = value;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string SecondName
        {
            get
            {
                return secondName;
            }
            set
            {
                secondName = value;
                OnPropertyChanged("SecondName");
            }
        }

        public string Patronymic
        {
            get
            {
                return patronymic;
            }
            set
            {
                patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }

        public DateTime DateBirth
        {
            get
            {
                return dateBirth;
            }
            set
            {
                dateBirth = value;
                OnPropertyChanged("DateBirth");
            }
        }

        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public string JobTitle
        {
            get
            {
                return jobTitle;
            }
            set
            {
                jobTitle = value;
                OnPropertyChanged("JobTitle");
            }
        }

        public string SubdivisionName
        {
            get
            {
                return subdivisionName;
            }
            set
            {
                subdivisionName = value;
                OnPropertyChanged("SubdivisionName");
            }
        }

        public string DepartamentHeadName
        {
            get
            {
                return departamentHeadName;
            }
            set
            {
                departamentHeadName = value;
                OnPropertyChanged("DepartamentHeadName");
            }
        }

        private void GetField(object obj)
        {
            var message = (EditMessage)obj;
            flagNewEmployee = false;

            FirstName = message.Employee.FirstName;
            SecondName = message.Employee.SecondName;
            Patronymic = message.Employee.Patronymic;
            DateBirth = message.Employee.DateBirth;
            Gender = message.Employee.Gender;
            JobTitle = message.Employee.JobTitle;
            SubdivisionName = message.Employee.SubdivisionName;
            DepartamentHeadName = message.Employee.DepartamentHeadName;

            currentEmployee = message.Employee;
        }

        private void SaveEmployee()
        {
            if (flagNewEmployee)
            {
                FillEmployee();
                repository.Add(currentEmployee);
            }
            else
            {
                FillEmployee();
                repository.Edit(currentEmployee);
            }
        }

        private void PromoteEmployee()
        {
            if (currentEmployee == null)
                return;
            currentEmployee.Promote();
            JobTitle = currentEmployee.JobTitle;
        }

        private void DemoteEmployee()
        {
            if (currentEmployee == null)
                return;
            currentEmployee.Demote();
            JobTitle = currentEmployee.JobTitle;
        }

        private void FillEmployee()
        {
            if (flagNewEmployee)
                currentEmployee = new Employee();

            currentEmployee.FirstName = FirstName;
            currentEmployee.SecondName = SecondName;
            currentEmployee.Patronymic = Patronymic;
            currentEmployee.DateBirth = DateBirth;
            currentEmployee.Gender = Gender;
            currentEmployee.JobTitle = JobTitle;
            currentEmployee.SubdivisionName = SubdivisionName;
            currentEmployee.DepartamentHeadName = DepartamentHeadName;
        }

        private void ClearField()
        {
            FirstName = "";
            SecondName = "";
            Patronymic = "";
            DateBirth = DateTime.Now;
            Gender = "";
            JobTitle = "";
            SubdivisionName = "";
            DepartamentHeadName = "";
        }
        // Window
        private void CloseWindow()
        {
            currentEmployee = null;
            windowService.CloseWindow(windowId);
        }

        private void MaximizeWindow()
        {
            windowService.MaximizeWindow(windowId);
        }

        private void MinimizeWindow()
        {
            windowService.MinimizeWindow(windowId);
        }

        private void GetId(object obj)
        {
            var message = (GetIdMessage)obj;
            if (windowId == -1)
                windowId = message.Id;
        }

        // Window

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
