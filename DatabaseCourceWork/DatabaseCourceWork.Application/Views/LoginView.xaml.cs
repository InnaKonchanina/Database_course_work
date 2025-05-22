using DatabaseCourceWork.DesktopApplication.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DatabaseCourceWork.DesktopApplication.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Password = PasswordBox.Password;
                vm.ValidateAll();
            }
        }
    }
}
