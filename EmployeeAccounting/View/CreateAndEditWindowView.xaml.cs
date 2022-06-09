using System.Windows;

namespace EmployeeAccounting.View
{
    /// <summary>
    /// Логика взаимодействия для CreateAndEditWindowView.xaml
    /// </summary>
    public partial class CreateAndEditWindowView : Window
    {
        public CreateAndEditWindowView()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
