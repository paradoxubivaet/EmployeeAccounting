using System;
using System.Windows;

namespace EmployeeAccounting.Services
{
    public class WindowService : IWindowService
    {
        public static int Counter { get; private set; } = 0;
        public void ShowWindow(string windowName)
        {
            var type = Type.GetType($"EmployeeAccounting.View.{windowName}");
            var window = (Window)Activator.CreateInstance(type);

            Counter += 1;
            DictionaryWindows.Add(window, Counter);

            window.Show();
        }

        public void CloseWindow(int id)
        {
            DictionaryWindows.Remove(id);
        }

        public void MaximizeWindow(int id)
        {
            var window = DictionaryWindows.GetWindow(id);

            if (window.WindowState == WindowState.Normal)
                window.WindowState = WindowState.Maximized;
            else if (window.WindowState == WindowState.Maximized)
                window.WindowState = WindowState.Normal;
            else if (window.WindowState == WindowState.Minimized)
                window.WindowState = WindowState.Normal;
        }

        public void MinimizeWindow(int id)
        {
            var window = DictionaryWindows.GetWindow(id);

            if (window.WindowState == WindowState.Normal)
                window.WindowState = WindowState.Minimized;
            else if (window.WindowState == WindowState.Maximized)
                window.WindowState = WindowState.Minimized;
            else if (window.WindowState == WindowState.Minimized)
                window.WindowState = WindowState.Normal;
        }
    }
}
