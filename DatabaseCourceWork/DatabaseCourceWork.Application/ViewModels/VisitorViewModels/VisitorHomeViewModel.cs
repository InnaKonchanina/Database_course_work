using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using DatabaseCourceWork.DesktopApplication.ViewModels.Reused;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.VisitorViewModels
{
    internal class VisitorHomeViewModel : BaseHomeViewModel
    {

        public VisitorHomeViewModel(User user, MainWindowViewModel mainWindowViewModel)
            : base(user, mainWindowViewModel)
        {
            AllEvents = new EventsCardsViewModel("All Events", mainWindowViewModel, () =>
            {
                MyEvents?.Refresh();
            })
            {
                DisplayVisitors = System.Windows.Visibility.Collapsed
            };

            MyEvents = new EventsCardsViewModel("All Events", mainWindowViewModel, () =>
            {
                AllEvents?.Refresh();
            })
            { 
                DisplayVisitors = System.Windows.Visibility.Collapsed
            };

            MyEvents.Refresh();
            AllEvents.Refresh();
        }

        public EventsCardsViewModel AllEvents { get; }

        public EventsCardsViewModel MyEvents { get; }
    }
}
