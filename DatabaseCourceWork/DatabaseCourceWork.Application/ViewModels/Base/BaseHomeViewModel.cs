using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.Base
{
    internal partial class BaseHomeViewModel : BaseViewModel
    {
        protected BaseHomeViewModel(User user, MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {
            this.user = new UserViewModel(user);
        }

        [ObservableProperty]
        private UserViewModel user;

        public BaseViewModel RoleSpecificContent => this;

        [RelayCommand]
        private void Logout()
        {
            _mainWindowViewModel.NavigateToLogin();
        }

        [RelayCommand]
        private void Refresh()
        {
            RoleSpecificContent?.Refresh();
        }
    }
}
