using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.Utils.DatabaseCourceWork.DesktopApplication.Services;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels;
using Microsoft.Win32;
using System.IO;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.OrganizerViewModels
{
    internal partial class NewLocationViewModel : BaseViewModel
    {
        public NewLocationViewModel(MainWindowViewModel mainWindowViewModel)
            : base(mainWindowViewModel)
        {
            NewLocation = new LocationViewModel(new Location());
        }

        [ObservableProperty]
        private LocationViewModel newLocation;

        [ObservableProperty]
        private string validationErrorMessage = string.Empty;

        public event EventHandler SaveCompleted;

        [RelayCommand]
        private void Save()
        {
            ValidationErrorMessage = string.Empty;

            if (!Validate(out var errors))
            {
                ValidationErrorMessage = string.Join(Environment.NewLine, errors);
                OnPropertyChanged(nameof(ValidationErrorMessage));
                return;
            }

            DatabaseManager.Instance.AddLocation(NewLocation.ToModel());
            MessageBoxProvider.Instance.Show("Location saved successfully!");
            SaveCompleted?.Invoke(this, EventArgs.Empty);
            ResetNewLocation();
        }

        [RelayCommand]
        private void Cancel()
        {
            ResetNewLocation();
        }

        [RelayCommand]
        private void SelectPhoto()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                NewLocation.Photo = imageBytes;
            }
        }

        private bool Validate(out List<string> errors)
        {
            errors = new();

            if (string.IsNullOrWhiteSpace(NewLocation.Name))
                errors.Add("Name is required.");

            if (string.IsNullOrWhiteSpace(NewLocation.Description))
                errors.Add("Description is required.");

            if (string.IsNullOrWhiteSpace(NewLocation.Address))
                errors.Add("Address is required.");

            if (NewLocation.Capacity <= 0)
                errors.Add("Capacity must be greater than 0.");

            if (NewLocation.Photo == null || NewLocation.Photo.Length == 0)
                errors.Add("Photo must be selected.");

            return errors.Count == 0;
        }

        private void ResetNewLocation()
        {
            NewLocation = new LocationViewModel(new Location());

            ValidationErrorMessage = string.Empty;
            OnPropertyChanged(nameof(ValidationErrorMessage));
        }

    }
}
