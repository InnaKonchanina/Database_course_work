using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels;
using System.Collections.ObjectModel;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.Reused
{
    internal partial class EventsCardsViewModel : BaseViewModel
    {
        private readonly Action _refreshOther;

        protected EventsCardsViewModel(MainWindowViewModel mainWindowViewModel, Action refreshOther) : base(mainWindowViewModel)
        {
            _refreshOther = refreshOther;
        }

        public ObservableCollection<CulturalEventViewModel> Events { get; }

        [ObservableProperty]
        private bool isUpcomingVisible;
        [ObservableProperty]
        private bool isOldVisible;

        private void RefreshEvents()
        {
            // Reload events or add the newly created event to collections
            Events.Clear();
            IEnumerable<CulturalEventViewModel> allEvents = DatabaseManager.Instance.GetAllEvents()
                            .Select(evm => new CulturalEventViewModel(evm)).OrderBy(e => e.StartDateTime);

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

            _refreshOther();
        }
    }
}
