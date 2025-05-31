using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.Reused
{
    internal partial class EventsCardsViewModel : BaseViewModel
    {
        private readonly Func<CulturalEvent, bool> _eventFilter;
        private readonly Action _refreshOther;

        public EventsCardsViewModel(Func<CulturalEvent, bool> eventFilter, UserViewModel activeUser, string title,
            MainWindowViewModel mainWindowViewModel, Action refreshOther) : base(mainWindowViewModel)
        {
            _eventFilter = eventFilter;
            ActiveUser = activeUser;
            Events = new ObservableCollection<CulturalEventViewModel>();
            Title = title;
            _refreshOther = refreshOther;
            IsOldVisible = true;
            IsUpcomingVisible = true;
            DisplayVisitors = Visibility.Visible;
        }

        [ObservableProperty]
        private UserViewModel activeUser;

        [ObservableProperty]
        private string title;
        public ObservableCollection<CulturalEventViewModel> Events { get; }

        public bool AllowLeaveFeedback { get; init; }

        public bool AllowJoinEvent { get; init; }

        [ObservableProperty]
        private bool isUpcomingVisible;
        [ObservableProperty]
        private bool isOldVisible;

        [ObservableProperty]
        private Visibility displayVisitors;

        partial void OnIsOldVisibleChanged(bool value)
        {
            Refresh(true);
        }

        partial void OnIsUpcomingVisibleChanged(bool value)
        {
            Refresh(true);
        }

        public void Refresh(bool refreshOther = false)
        {
            // Reload events or add the newly created event to collections
            Events.Clear();
            IEnumerable<CulturalEventViewModel> allEvents = DatabaseManager.Instance.GetAllEvents().
                Where(e => _eventFilter(e)).
                Select(evm => new CulturalEventViewModel(ActiveUser, evm, () => Refresh(true), AllowLeaveFeedback, AllowJoinEvent)).OrderBy(e => e.StartDateTime);

            foreach (var ev in allEvents)
            {
                if (IsUpcomingVisible && ev.StartDateTime >= DateTime.Now)
                {
                    Events.Add(ev);
                }

                if (IsOldVisible && ev.StartDateTime < DateTime.Now)
                {
                    Events.Add(ev);
                }
            }

            if (refreshOther)
                _refreshOther();
        }
    }
}
