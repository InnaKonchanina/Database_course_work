using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.Reused
{
    internal partial class EventsCardsViewModel : BaseViewModel
    {
        private readonly Action _refreshOther;

        public EventsCardsViewModel(string title, MainWindowViewModel mainWindowViewModel, Action refreshOther) : base(mainWindowViewModel)
        {
            Events = new ObservableCollection<CulturalEventViewModel>();
            Title = title;
            _refreshOther = refreshOther;
            IsOldVisible = true;
            IsUpcomingVisible = true;
            DisplayVisitors = Visibility.Visible;
        }

        [ObservableProperty]
        private string title;
        public ObservableCollection<CulturalEventViewModel> Events { get; }

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
                Select(evm => new CulturalEventViewModel(evm)).OrderBy(e => e.StartDateTime);

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
