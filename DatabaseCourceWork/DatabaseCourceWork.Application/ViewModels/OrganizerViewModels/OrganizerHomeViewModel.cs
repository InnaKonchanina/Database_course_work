using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels;
using System.Collections.ObjectModel;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.OrganizerViewModels
{
    internal partial class OrganizerHomeViewModel : BaseHomeViewModel
    {
        public OrganizerHomeViewModel(User user, MainWindowViewModel mainWindowViewModel)
            : base(user, mainWindowViewModel)
        {
            Locations = new ObservableCollection<LocationViewModel>(
                DatabaseManager.Instance.GetAllLocations().Select(l => new LocationViewModel(l)));
            Artists = new ObservableCollection<UserViewModel>(
                DatabaseManager.Instance.GetAllArtist().Select(l => new UserViewModel(l)));
            AllEvents = new ObservableCollection<CulturalEventViewModel>();
            MyEvents = new ObservableCollection<CulturalEventViewModel>();
            RefreshEvents();


            NewEvent = new NewEventViewModel(mainWindowViewModel, User, Locations,
                new ObservableCollection<SelectableUserViewModel>(Artists.Select(a => new SelectableUserViewModel(a))));
            NewEvent.SaveCompleted += OnNewEventSaved;
        }

        public ObservableCollection<LocationViewModel> Locations { get; }

        public ObservableCollection<UserViewModel> Artists { get; }

        public ObservableCollection<CulturalEventViewModel> AllEvents { get; }

        public ObservableCollection<CulturalEventViewModel> MyEvents { get; }

        [ObservableProperty]
        private NewEventViewModel newEvent;

        private void OnNewEventSaved(object sender, EventArgs e)
        {
            RefreshEvents();
        }

        private void RefreshEvents()
        {
            // Reload events or add the newly created event to collections
            AllEvents.Clear();
            foreach (var ev in DatabaseManager.Instance.GetAllEvents()
                .Select(evm => new CulturalEventViewModel(evm)))
            {
                AllEvents.Add(ev);
            }

            MyEvents.Clear();
            foreach (var ev in AllEvents.Where(ev => ev.OrganizerId == User.Id))
            {
                MyEvents.Add(ev);
            }
        }
    }
}
