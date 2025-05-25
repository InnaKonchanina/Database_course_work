using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.OrganizerViewModels
{
    internal class OrganizerHomeViewModel : BaseHomeViewModel
    {
        public OrganizerHomeViewModel(User user, MainWindowViewModel mainWindowViewModel)
            : base(user, mainWindowViewModel)
        {

        }
    }
}
