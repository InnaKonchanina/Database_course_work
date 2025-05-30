using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;
using DatabaseCourceWork.DesktopApplication.ViewModels.Reused;
using System.Windows;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.ArtistViewModels
{
    internal class ArtistHomeViewModel : BaseHomeViewModel
    {
        public ArtistHomeViewModel(User user, MainWindowViewModel mainWindowViewModel)
            : base(user, mainWindowViewModel)
        {
            MyEvents = new EventsCardsViewModel(e => e.Artists.Any(v => v.Id == User.Id), User, "My Events", mainWindowViewModel, () => { })
            {
                DisplayVisitors = Visibility.Visible,
                AllowLeaveFeedback = true,
            };

            MyEvents.Refresh();
        }

        public EventsCardsViewModel MyEvents { get; }

        public override void Refresh()
        {
            base.Refresh();

            MyEvents.Refresh();
        }
    }
}