using System.Windows;
using System.Windows.Input;

namespace EmployeeAccounting.View
{
    /// <summary>
    /// Логика взаимодействия для FilterWindowView.xaml
    /// </summary>
    public partial class FilterWindowView : Window
    {
        public FilterWindowView()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
