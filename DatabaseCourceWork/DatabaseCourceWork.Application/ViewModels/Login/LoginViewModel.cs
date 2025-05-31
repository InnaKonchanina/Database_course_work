using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.Utils.DatabaseCourceWork.DesktopApplication.Services;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using DatabaseCourceWork.DesktopApplication.Views.Login;
using System.ComponentModel.DataAnnotations;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.Login
{
    internal partial class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(MainWindowViewModel mainWindowViewModel)
            : base(mainWindowViewModel)
        {
            // Email = "eve.visitor@example.com";
            //Email = "stas@stas.com";
        }

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

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private void Login()
        {
            User? user = DatabaseManager.Instance.TryLogin(Email, Password);

            if (user == null)
            {
                MessageBoxProvider.Instance.ShowWarning("Failed to login");
            }
            else
            {
                _mainWindowViewModel.NavigateToHome(user);
            }
        }

        [RelayCommand]
        private void OpenRegister()
        {
            var registerWindow = new RegisterWindow()
            {
                DataContext = new RegisterViewModel(_mainWindowViewModel)
            };
            registerWindow.ShowDialog();
        }
    }
}
