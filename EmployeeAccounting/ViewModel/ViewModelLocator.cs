using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace EmployeeAccounting.ViewModel
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel
        {
            get
            {
                return Ioc.Default.GetService<MainWindowViewModel>();
            }
        }

        public CreateAndEditWindowViewModel CreateAndEditWindowViewModel
        {
            get
            {
                return Ioc.Default.GetService<CreateAndEditWindowViewModel>();
            }
        }

        public FilterWindowViewModel FilterWindowViewModel
        {
            get
            {
                return Ioc.Default.GetService<FilterWindowViewModel>();
            }
        }
    }
}
