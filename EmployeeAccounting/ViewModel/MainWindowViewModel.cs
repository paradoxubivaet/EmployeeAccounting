using EmployeeAccounting.DAL.EntityFramework.Model;
using EmployeeAccounting.Messages;
using EmployeeAccounting.Model;
using EmployeeAccounting.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace EmployeeAccounting.ViewModel
{
    public class MainWindowViewModel  : INotifyPropertyChanged
    {
        private EmployeesCollectionModel employeesCollection;
        private Employee selectedEmployee;

        private IWindowService windowService;
        private IMessenger messenger;
        private ICollectionView collectionView; 
        private IEmployeeRepository repository;

        public ControllCommand InvokeCreateWindowCommand { get; private set; }
        public ControllCommand MinimizeWindowCommand { get; private set; }
        public ControllCommand MaximizeWindowCommand { get; private set; }
        public ControllCommand CloseWindowCommand { get; private set; }
        public ControllCommand InvokeFilterWindowCommand { get; private set; }
        public ControllCommandWithParameter InvokeEditWindowCommand { get; private set; }
        public ControllCommandWithParameter InvokeDeleteEmployeeCommand { get; private set; }

        public MainWindowViewModel(IWindowService windowService, IMessenger messenger, IEmployeeRepository repository)
        {
            this.windowService = windowService;
            this.messenger = messenger;
            this.repository = repository;

            employeesCollection = EmployeesCollectionModel.employeesCollection;
            collectionView = CollectionViewSource.GetDefaultView(this.EmployeesCollection);

            InvokeCreateWindowCommand = new ControllCommand(InvokeCreateWindow);
            InvokeFilterWindowCommand = new ControllCommand(InvokeFilterWindow);
            InvokeEditWindowCommand = new ControllCommandWithParameter(InvokeEditWindow);
            InvokeDeleteEmployeeCommand = new ControllCommandWithParameter(DeleteEmployee);

            MinimizeWindowCommand = new ControllCommand(MinimizeWindow);
            MaximizeWindowCommand = new ControllCommand(MaximizeWindow);
            CloseWindowCommand = new ControllCommand(CloseWindow);

            messenger.Subscribe<UpdateMessage>(this, UpdateView);
            messenger.Subscribe<SortMessage>(this, ApplySortSettings);
            messenger.Subscribe<NotificationClosingMessage>(this, DropSortSettings);
        }

        public ObservableCollection<Employee> EmployeesCollection
        {
            get
            {
                return employeesCollection.EmployeesCollection;
            }
            set
            {
                employeesCollection.EmployeesCollection = value;
                OnPropertyChanged("EmployeesCollection");
            }
        }

        public Employee SelectedEmployee
        {
            get
            {
                return selectedEmployee;
            }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        private void InvokeCreateWindow()
        {
            windowService.ShowWindow("CreateAndEditWindowView");
        }

        private void InvokeFilterWindow()
        {
            windowService.ShowWindow("FilterWindowView");
            messenger.Send(CreateFilterMessage());
        }

        private FilterMessage CreateFilterMessage()
        { 
            List<string> divisions = repository.GetDivisions();
            List<string> jobTitles = JobTitles.GetJobTitles();

            FilterMessage message = new FilterMessage(divisions, jobTitles);
            return message;
        }

        private void InvokeEditWindow(Employee employee)
        {
            if (employee == null)
                return;
            windowService.ShowWindow("CreateAndEditWindowView");
            messenger.Send(new EditMessage(ref employee));
        }

        private void DeleteEmployee(Employee employee)
        {
            if (employee != null)
                repository.Delete(employee);
        }

        private void UpdateView(object obj)
        {
            var message = (UpdateMessage)obj;
            if (message.Flag == true)
            {
                EmployeesCollection = employeesCollection.EmployeesCollection;
                collectionView = CollectionViewSource.GetDefaultView(this.EmployeesCollection);
            }
        }

        private void ApplySortSettings(object obj)
        {
            var message = (SortMessage)obj;
            SortView(message.Division, message.JobTitle);
        }

        private void SortView(string division, string jobTitle)
        {
            if (division == "" && jobTitle == "")
                return;
            if (division == null && jobTitle == null)
                return;

            if (division == null && jobTitle != "") 
                collectionView.Filter = item => (item as Employee).JobTitle.Contains(jobTitle);
            else if(division != "" && jobTitle == null)
                collectionView.Filter = item => (item as Employee).SubdivisionName.Contains(division);
            else if (division != null && jobTitle != null)
                collectionView.Filter = item => (item as Employee).JobTitle.Contains(jobTitle) && 
                                                (item as Employee).SubdivisionName.Contains(division);
        }

        private void DropSortSettings(object obj)
        {
            collectionView.Filter = null;
        }

        // Window

        private void MinimizeWindow()
        {
            App.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow()
        {
            var currentState = Application.Current.MainWindow.WindowState;
            if (currentState == WindowState.Normal)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else if (currentState == WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            else if (currentState == WindowState.Minimized)
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void CloseWindow()
        {
            Application.Current.Shutdown();
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
