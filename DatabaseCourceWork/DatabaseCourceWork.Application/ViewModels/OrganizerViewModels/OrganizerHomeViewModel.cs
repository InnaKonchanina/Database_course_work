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
            AllEvents = new ObservableCollection<CulturalEventViewModel>();
            MyEvents = new ObservableCollection<CulturalEventViewModel>();
            RefreshEvents();

            NewEvent = new NewEventViewModel(mainWindowViewModel, User, Locations,
                new ObservableCollection<SelectableUserViewModel>(Artists.Select(a => new SelectableUserViewModel(a))));
            NewEvent.SaveCompleted += OnNewEventSaved;

            IsAllEventsOldVisible = true;
            IsAllEventsUpcomingVisible = true;

            IsMyEventsOldVisible = true;
            IsMyEventsUpcomingVisible = true;
        }

        public ObservableCollection<LocationViewModel> Locations { get; }

        public ObservableCollection<UserViewModel> Artists { get; }

        public ObservableCollection<CulturalEventViewModel> AllEvents { get; }

        public ObservableCollection<CulturalEventViewModel> MyEvents { get; }

        public EventsCardsViewModel MyEventsCards { get; }

        [ObservableProperty]
        private bool isMyEventsUpcomingVisible;
        [ObservableProperty]
        private bool isMyEventsOldVisible;

        [ObservableProperty]
        private bool isAllEventsUpcomingVisible;
        [ObservableProperty]
        private bool isAllEventsOldVisible;

        [ObservableProperty]
        private NewEventViewModel newEvent;

        private void OnNewEventSaved(object? sender, EventArgs e)
        {
            this.RefreshEvents();
        }

        partial void OnIsAllEventsOldVisibleChanged(bool value)
        {
            RefreshEvents(); ;
        }

        partial void OnIsAllEventsUpcomingVisibleChanged(bool value)
        {
            RefreshEvents();
        }

        partial void OnIsMyEventsOldVisibleChanged(bool value)
        {
            RefreshEvents();
        }

        partial void OnIsMyEventsUpcomingVisibleChanged(bool value)
        {
            RefreshEvents();
        }

        private void RefreshEvents()
        {
            Locations.Clear();
            foreach (var item in DatabaseManager.Instance.GetAllLocations().Select(l => new LocationViewModel(l)))
            {
                Locations.Add(item);
            } ;
            // Reload events or add the newly created event to collections
            AllEvents.Clear();
            IEnumerable<CulturalEventViewModel> allEvents = DatabaseManager.Instance.GetAllEvents()
                            .Select(evm => new CulturalEventViewModel(evm)).OrderBy(e => e.StartDateTime);

            foreach (var ev in allEvents)
            {
                if (IsAllEventsUpcomingVisible && ev.StartDateTime >= DateTime.Now)
                {
                    AllEvents.Add(ev);
                }

                if (IsAllEventsOldVisible && ev.StartDateTime < DateTime.Now)
                {
                    AllEvents.Add(ev);
                }
            }

            MyEvents.Clear();
            foreach (var ev in allEvents.Where(ev => ev.OrganizerId == User.Id))
            {
                if (IsMyEventsUpcomingVisible && ev.StartDateTime >= DateTime.Now)
                {
                    MyEvents.Add(ev);
                }

                if (IsMyEventsOldVisible && ev.StartDateTime < DateTime.Now)
                {
                    MyEvents.Add(ev);
                }
            }
        }
    }
}
