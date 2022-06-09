using EmployeeAccounting.Messages;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace EmployeeAccounting.Services
{
    public static class DictionaryWindows
    {
        public static Dictionary<int, Window> Windows { get; }

        static DictionaryWindows()
        {
            Windows = new Dictionary<int, Window>();
        }

        public static void Add(Window window, int counter)
        {
            var messenger = Ioc.Default.GetService<IMessenger>();
            Windows.Add(counter, window);

            messenger.Send(new GetIdMessage(counter));
        }

        public static void Remove(int windowId)
        {
            if (Windows.ContainsKey(windowId))
            {
                Windows[windowId].Close();
                Windows.Remove(windowId);
            }
        }
        public static Window GetWindow(int windowId)
        {
            return Windows[windowId];
        }
    }
}
