using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseCourceWork.DesktopApplication.Database.Models;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.Base
{
    internal partial class BaseHomeViewModel : BaseViewModel
    {
        protected BaseHomeViewModel(User user, MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {
            this.user = user;
        }

        public BaseViewModel RoleSpecificContent => this;

        [ObservableProperty]
        private User user;

        [RelayCommand]
        private void Logout()
        {
            _mainWindowViewModel.NavigateToLogin();
        }
    }
}
