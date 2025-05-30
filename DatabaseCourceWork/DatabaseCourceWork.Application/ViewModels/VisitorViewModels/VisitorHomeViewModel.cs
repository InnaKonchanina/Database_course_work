using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using DatabaseCourceWork.DesktopApplication.ViewModels.Reused;
using System.Windows;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.VisitorViewModels
{
    internal class VisitorHomeViewModel : BaseHomeViewModel
    {
        public VisitorHomeViewModel(User user, MainWindowViewModel mainWindowViewModel)
            : base(user, mainWindowViewModel)
        {
            AllEvents = new EventsCardsViewModel(e => true, User, "All Events", mainWindowViewModel, () =>
            {
                MyEvents?.Refresh();
            })
            {
                DisplayVisitors = Visibility.Collapsed,
                AllowJoinEvent = true,
                AllowLeaveFeedback = true,
            };

            MyEvents = new EventsCardsViewModel(e => e.Visitors.Any(v => v.Id == User.Id), User, "All Events", mainWindowViewModel, () =>
            {
                AllEvents?.Refresh();
            })
            {
                DisplayVisitors = Visibility.Collapsed,
                AllowJoinEvent = true,
                AllowLeaveFeedback = true,
            };

            MyEvents.Refresh();
            AllEvents.Refresh();
        }

        public EventsCardsViewModel AllEvents { get; }

        public EventsCardsViewModel MyEvents { get; }

        public override void Refresh()
        {
            base.Refresh();

            MyEvents.Refresh();
            AllEvents.Refresh();
        }
    }
}
