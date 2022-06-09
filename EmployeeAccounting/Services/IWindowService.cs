namespace EmployeeAccounting.Services
{
    public interface IWindowService
    {
        public void ShowWindow(string nameWindow);
        void CloseWindow(int id);
        void MaximizeWindow(int id);
        void MinimizeWindow(int id);
    }
}
