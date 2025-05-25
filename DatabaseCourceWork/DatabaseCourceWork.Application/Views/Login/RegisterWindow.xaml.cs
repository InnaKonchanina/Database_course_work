using DatabaseCourceWork.DesktopApplication.ViewModels.Login;
using System.Windows;
using System.Windows.Controls;

namespace DatabaseCourceWork.DesktopApplication.Views.Login
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel vm)
            {
                vm.Password = PasswordBox.Password;
                vm.ValidateAll();
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel vm)
            {
                vm.ConfirmPassword = ConfirmPasswordBox.Password;
                vm.ValidateAll();
            }
        }
    }
}
