using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.ViewModels.Base;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.VisitorViewModels
{
    internal class VisitorHomeViewModel : BaseHomeViewModel
    {

        public VisitorHomeViewModel(User user, MainWindowViewModel mainWindowViewModel)
            : base(user, mainWindowViewModel)
        {

        }
    }
}
