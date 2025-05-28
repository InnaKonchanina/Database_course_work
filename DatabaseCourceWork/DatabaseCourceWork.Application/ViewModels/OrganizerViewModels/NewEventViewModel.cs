using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.Utils.DatabaseCourceWork.DesktopApplication.Services;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.OrganizerViewModels
{
    internal partial class NewEventViewModel : BaseViewModel
    {
        public NewEventViewModel(MainWindowViewModel mainWindowViewModel, UserViewModel organizer,
                             ObservableCollection<LocationViewModel> locations,
                             ObservableCollection<SelectableUserViewModel> allArtists)
            :base(mainWindowViewModel)
        {
            Organizer = organizer;
            Locations = locations;
            Artists = new ObservableCollection<SelectableUserViewModel>(
                allArtists.Select(a => new SelectableUserViewModel(a.User)));

            NewEvent = new CulturalEventViewModel
            {
                OrganizerId = Organizer.Id,
                Organizer = Organizer,
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1),
                Location = Locations.FirstOrDefault()
            };

            SelectedArtists = new ObservableCollection<SelectableUserViewModel>();
        }

        public UserViewModel Organizer { get; }

        public ObservableCollection<LocationViewModel> Locations { get; }

        public ObservableCollection<SelectableUserViewModel> Artists { get; }

        [ObservableProperty]
        private CulturalEventViewModel newEvent;

        [ObservableProperty]
        private ObservableCollection<SelectableUserViewModel> selectedArtists;

        [ObservableProperty]
        private string validationErrorMessage = string.Empty;


        [RelayCommand]
        public void SaveNewEvent()
        {
            validationErrorMessage = string.Empty;

            // Gather selected artists for new event
            NewEvent.Artists = Artists.Where(a => a.IsSelected).Select(a => a.User).ToList();

            if (!Validate(out var errors))
            {
                validationErrorMessage = string.Join(Environment.NewLine, errors);
                OnPropertyChanged(nameof(ValidationErrorMessage));
                return;
            }

            // Save to DB
            DatabaseManager.Instance.AddEvent(NewEvent.ToModel());

            // Reset form for next event
            NewEvent = new CulturalEventViewModel
            {
                OrganizerId = Organizer.Id,
                Organizer = Organizer,
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1),
                Location = Locations.FirstOrDefault()
            };

            ResetNewEvent();

            foreach (var artist in Artists)
                artist.IsSelected = false;

            SelectedArtists.Clear();
            MessageBoxProvider.Instance.Show("Event saved successfully!");
            // Optionally notify OrganizerHomeViewModel to refresh event lists
            SaveCompleted?.Invoke(this, EventArgs.Empty);
        }

        private bool Validate(out List<string> errors)
        {
            errors = new();

            if (string.IsNullOrWhiteSpace(NewEvent.Title))
                errors.Add("Title is required.");

            if (string.IsNullOrWhiteSpace(NewEvent.Description))
                errors.Add("Description is required."); // <-- added this check

            if (NewEvent.StartDateTime == default)
                errors.Add("Start date/time is required.");

            if (NewEvent.EndDateTime == default)
                errors.Add("End date/time is required.");

            if (NewEvent.StartDateTime > NewEvent.EndDateTime)
                errors.Add("Start date/time must be before End date/time.");

            if (NewEvent.Location == null)
                errors.Add("Location must be selected.");

            if (NewEvent.Artists == null || NewEvent.Artists.Count == 0)
                errors.Add("At least one artist must be selected.");

            return errors.Count == 0;
        }


        private void ResetNewEvent()
        {
            NewEvent = new CulturalEventViewModel
            {
                OrganizerId = Organizer.Id,
                Organizer = Organizer,
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1),
                Location = Locations.FirstOrDefault(),
            };

            foreach (var artist in Artists)
                artist.IsSelected = false;

            SelectedArtists.Clear();
            validationErrorMessage = string.Empty;
            OnPropertyChanged(nameof(ValidationErrorMessage));
        }

        public event EventHandler SaveCompleted;
    }
}
