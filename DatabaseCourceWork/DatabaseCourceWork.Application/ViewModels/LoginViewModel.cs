using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using DatabaseCourceWork.DesktopApplication.Views;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace DatabaseCourceWork.DesktopApplication.ViewModels
{
    internal partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string email;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        [NotifyPropertyChangedFor(nameof(PasswordError))]
        private string password;

        public string PasswordError => GetFirstError(nameof(Password));

        public bool CanLogin =>
            !string.IsNullOrWhiteSpace(Email) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !HasErrors;

        public LoginViewModel()
        {

        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private void Login()
        {
            // Example login logic
            System.Windows.MessageBox.Show($"Logging in as {Email}");
        }

        [RelayCommand]
        private void OpenRegister()
        {
            var registerWindow = new RegisterWindow()
            {
                DataContext = new RegisterViewModel()
            };
            registerWindow.ShowDialog();
        }
    }
}
