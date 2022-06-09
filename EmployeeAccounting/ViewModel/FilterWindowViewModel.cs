using EmployeeAccounting.Messages;
using EmployeeAccounting.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeAccounting.ViewModel
{
    public  class FilterWindowViewModel : INotifyPropertyChanged
    {
        private List<string> divisions;
        private List<string> jobTitles;
        private string selectedDivision;
        private string selectedJobTitle;
        private int windowId;

        private IMessenger messenger;
        private IWindowService windowService;

        public ControllCommand InvokeSendSortSettingCommand { get; private set; }
        public ControllCommand InvokeMinimizeWindowCommand { get; private set; }
        public ControllCommand InvokeMaximizeWindowCommand { get; private set; }
        public ControllCommand InvokeCloseWindowCommand { get; private set; }
        public ControllCommand InvokeWindowClosingCommand { get; private set; }

        public FilterWindowViewModel(IMessenger messenger, IWindowService windowService)
        {
            this.messenger = messenger;
            this.windowService = windowService;

            Divisions = new List<string>();
            jobTitles = new List<string>();

            InvokeSendSortSettingCommand = new ControllCommand(SendSortSettings);
            InvokeMinimizeWindowCommand = new ControllCommand(MinimizeWindow);
            InvokeMaximizeWindowCommand = new ControllCommand(MaximizeWindow);
            InvokeCloseWindowCommand = new ControllCommand(CloseWindow);
            InvokeWindowClosingCommand = new ControllCommand(SendClosingNotification);

            messenger.Subscribe<FilterMessage>(this, SetFilterLists);
            messenger.Subscribe<GetIdMessage>(this, GetId);

            windowId = -1;
        }

        public List<string> Divisions
        {
            get
            {
                return divisions; 
            }
            set
            {
                divisions = value;
                OnPropertyChanged("Divisions");
            }
        }

        public List<string> JobTitles
        {
            get
            {
                return jobTitles;
            }
            set
            {
                jobTitles = value;
                OnPropertyChanged("JobTitles");
            }
        }

        public string SelectedDivision
        {
            get
            {
                return selectedDivision;
            }
            set
            {
                selectedDivision = value;
                OnPropertyChanged("SelectedDivision");
            }
        }

        public string SelectedJobTitle
        {
            get
            {
                return selectedJobTitle;
            }
            set
            {
                selectedJobTitle = value;
                OnPropertyChanged("SelectedJobTitle");
            }
        }

        private void MinimizeWindow()
        {
            windowService.MinimizeWindow(windowId);
        }

        private void MaximizeWindow()
        {
            windowService.MaximizeWindow(windowId);
        }

        private void CloseWindow()
        {
            windowService.CloseWindow(windowId);
        }

        private void SendSortSettings()
        {
            SortMessage message = new SortMessage(SelectedDivision, SelectedJobTitle);
            messenger.Send(message);
        }

        private void SendClosingNotification()
        {
            messenger.Send(new NotificationClosingMessage());
        }

        private void SetFilterLists(object obj)
        {
            var message = (FilterMessage)obj;

            Divisions = message.Divisions;
            JobTitles = message.JobTitles;
        }
        private void GetId(object obj)
        {
            var message = (GetIdMessage)obj;
            if (windowId == -1)
                windowId = message.Id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
