using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels;
using DatabaseCourceWork.DesktopApplication.ViewModels.Reused;
using System.Collections.ObjectModel;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.OrganizerViewModels
{
    internal partial class OrganizerHomeViewModel : BaseHomeViewModel
    {
        public OrganizerHomeViewModel(User user, MainWindowViewModel mainWindowViewModel)
            : base(user, mainWindowViewModel)
        {
            Locations = new ObservableCollection<LocationViewModel>();
            Artists = new ObservableCollection<UserViewModel>(
               DatabaseManager.Instance.GetAllArtist().Select(l => new UserViewModel(l)));
            AllEvents = new EventsCardsViewModel(e => true, User, "All Events", mainWindowViewModel, () =>
            {
                MyEvents?.Refresh();
                RefreshLocations();
            });
            MyEvents = new EventsCardsViewModel(e => e.OrganizerId == User.Id, User, "All Events", mainWindowViewModel, () =>
            {
                AllEvents?.Refresh();
                RefreshLocations();
            });

            MyEvents.Refresh();
            AllEvents.Refresh();

            NewEvent = new NewEventViewModel(mainWindowViewModel, User, Locations,
                new ObservableCollection<SelectableUserViewModel>(Artists.Select(a => new SelectableUserViewModel(a))));
            NewEvent.SaveCompleted += OnNewEventSaved;

            NewLocation = new NewLocationViewModel(mainWindowViewModel);
            newLocation.SaveCompleted += NewLocation_SaveCompleted;

        }

        public ObservableCollection<LocationViewModel> Locations { get; }

        public ObservableCollection<UserViewModel> Artists { get; }

        public EventsCardsViewModel AllEvents { get; }

        public EventsCardsViewModel MyEvents { get; }

        [ObservableProperty]
        private NewEventViewModel newEvent;

        [ObservableProperty]
        private NewLocationViewModel newLocation;

        private void OnNewEventSaved(object? sender, EventArgs e)
        {
            MyEvents.Refresh();
            AllEvents.Refresh();
        }

        private void NewLocation_SaveCompleted(object? sender, EventArgs e)
        {
            RefreshLocations();
        }

        private void RefreshLocations()
        {
            Locations.Clear();
            foreach (var item in DatabaseManager.Instance.GetAllLocations().Select(l => new LocationViewModel(l)))
            {
                Locations.Add(item);
            }
        }

        public override void Refresh()
        {
            base.Refresh();

            MyEvents.Refresh();
            AllEvents.Refresh();

            RefreshLocations();
        }
    }
}
