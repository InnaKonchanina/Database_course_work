﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.Database.Models.Enums;
using DatabaseCourceWork.DesktopApplication.Utils.DatabaseCourceWork.DesktopApplication.Services;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.Login
{
    internal partial class RegisterViewModel : BaseViewModel
    {
        public RegisterViewModel(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {
            UserRoles = new ObservableCollection<string> { "Visitor", "Artist", "Organizer" };
            SelectedUserRole = UserRoles.First();
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
        private string selectedUserRole;

        public ObservableCollection<string> UserRoles { get; }

        [ObservableProperty]
        private string experience;

        [ObservableProperty]
        private string creativeInterests;

        public Visibility ShouldDetailsBeVisible
        {
            get
            {
                return (UserRole)Enum.Parse(typeof(UserRole), SelectedUserRole) == UserRole.Visitor ? Visibility.Hidden : Visibility.Visible;
            }
        }

        [RelayCommand(CanExecute = nameof(CanRegister))]
        private void Register()
        {
            try
            {
                if (Password != ConfirmPassword)
                {
                    MessageBoxProvider.Instance.ShowWarning("Passwords do not match.");
                    return;
                }

                if (DatabaseManager.Instance.CheckIfEmailIsUsed(Email))
                {
                    MessageBoxProvider.Instance.ShowWarning("Email is already registered.");
                    return;
                }

                ValidateAll();
                if (HasErrors)
                {
                    return;
                }
                DatabaseManager.Instance.RegisterNewUser(new User
                {
                    Email = Email,
                    PasswordHash = Password,
                    Experience = Experience,
                    CreativeInterests = CreativeInterests,
                    Role = SelectedUserRole.ToString(),
                    Name = Name,
                });

                // Simulate success
                MessageBoxProvider.Instance.Show("Registration successful!");
            }
            catch (Exception ex)
            {
                MessageBoxProvider.Instance.ShowError($"Registration failed: {ex.Message}", "Error");
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
