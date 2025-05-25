using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.Database.Models.Enums;
using DatabaseCourceWork.DesktopApplication.ViewModels.ArtistViewModels;
using DatabaseCourceWork.DesktopApplication.ViewModels.Login;
using DatabaseCourceWork.DesktopApplication.ViewModels.OrganizerViewModels;
using DatabaseCourceWork.DesktopApplication.ViewModels.VisitorViewModels;

namespace DatabaseCourceWork.DesktopApplication.ViewModels
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel()
        {
            NavigateToLogin();
        }

        [ObservableProperty]
        private ObservableObject currentViewModel;

        public void NavigateToHome(User user)
        {
            switch (user.UserRole)
            {
                case UserRole.Artist:
                    CurrentViewModel = new ArtistHomeViewModel(user, this);
                    break;
                case UserRole.Organizer:
                    CurrentViewModel = new OrganizerHomeViewModel(user, this);
                    break;
                case UserRole.Visitor:
                    CurrentViewModel = new VisitorHomeViewModel(user, this);
                    break;
                default:
                    throw new Exception("Unexpected user role");
            }
        }

        internal void NavigateToLogin()
        {
            CurrentViewModel = new LoginViewModel(this);
        }
    }
}
