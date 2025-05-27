using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.OrganizerViewModels
{
    internal partial class SelectableUserViewModel : ObservableObject
    {
        public SelectableUserViewModel(UserViewModel user)
        {
            User = user;
        }

        public UserViewModel User { get; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        public string Name => User.Name;
    }
}
