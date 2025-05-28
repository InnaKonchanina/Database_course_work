using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.Utils.DatabaseCourceWork.DesktopApplication.Services;
using DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.Reused
{
    internal partial class LeaveFeedbackViewModel : ObservableValidator
    {
        private readonly UserViewModel _userViewModel;
        private readonly CulturalEventViewModel _eventViewModel;

        public LeaveFeedbackViewModel(UserViewModel activeUser, CulturalEventViewModel culturalEventViewModel)
        {
            _eventViewModel = culturalEventViewModel;
            _userViewModel = activeUser;
        }

        [ObservableProperty]
        [Required]
        private int? rating;

        [ObservableProperty]
        [Required]
        private string? comment;

        [RelayCommand]
        private void Submit(Window window)
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                return;
            }

            if (Rating < 1 || Rating > 5)
            {
                MessageBox.Show("Rating must be between 1 and 5.");
                return;
            }

            DatabaseManager.Instance.LeaveFeedback(new Feedback
            {
                Comment = comment,
                CulturalEventId = _eventViewModel.Id,
                Rating = rating.Value,
                UserId = _userViewModel.Id
            });

            MessageBoxProvider.Instance.Show("Thank you! Your feedback has been saved successfully.");
            window.DialogResult = true;
            window?.Close();
        }

        [RelayCommand]
        private void Cancel(Window window)
        {
            window.DialogResult = false;
            window?.Close();
        }
    }
}
