using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.Database.Models.Enums;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace DatabaseCourceWork.DesktopApplication.ViewModels
{
    internal partial class RegisterViewModel : BaseViewModel
    {
        public RegisterViewModel()
        {
            UserRoles = new ObservableCollection<UserRole> { UserRole.Visitor, UserRole.Artist, UserRole.Organizer };
            UserRole = UserRoles.First();
        }

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Name is required")]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        private string name;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        private string email;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Password is required")]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        [NotifyPropertyChangedFor(nameof(PasswordError))]
        private string password;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Confirm Password is required")]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        [NotifyPropertyChangedFor(nameof(ConfirmPasswordError))]
        private string confirmPassword;

        public string PasswordError => GetFirstError(nameof(Password));

        public string ConfirmPasswordError => GetFirstError(nameof(ConfirmPassword));

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ShouldDetailsBeVisible))]
        private UserRole userRole;

        public ObservableCollection<UserRole> UserRoles { get; }

        [ObservableProperty]
        private string experience;

        [ObservableProperty]
        private string creativeInterests;

        public Visibility ShouldDetailsBeVisible
        {
            get
            {
                return UserRole == UserRole.Visitor ? Visibility.Hidden : Visibility.Visible;
            }
        }

        [RelayCommand(CanExecute = nameof(CanRegister))]
        private void Register()
        {
            try
            {
                if (Password != ConfirmPassword)
                {
                    MessageBox.Show("Passwords do not match.");
                    return;
                }

                if (DatabaseManager.Instance.CheckIfEmailIsUsed(Email))
                {
                    MessageBox.Show("Email is already registered.");
                    return;
                }

                DatabaseManager.Instance.RegisterNewUser(new User
                {
                    Email = Email,
                    PasswordHash = Password,
                    Experience = Experience,
                    CreativeInterests = CreativeInterests,
                    Role = UserRole.ToString(),
                    Name = Name,
                });

                // Simulate success
                MessageBox.Show("Registration successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool CanRegister()
        {
            return !HasErrors &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                   !string.IsNullOrWhiteSpace(Name);
        }
    }
}
