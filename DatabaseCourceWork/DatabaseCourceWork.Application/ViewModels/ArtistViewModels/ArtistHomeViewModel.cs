using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.ArtistViewModels
{
    internal class ArtistHomeViewModel : BaseHomeViewModel
    {
        public ArtistHomeViewModel(User user, MainWindowViewModel mainWindowViewModel) : base(user, mainWindowViewModel)
        {
        }
    }
}
