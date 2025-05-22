using CommunityToolkit.Mvvm.ComponentModel;

namespace DatabaseCourceWork.DesktopApplication.ViewModels
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel()
        {
            Title = "Hello MVVM!";
            CurrentViewModel = new LoginViewModel();
        }

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private ObservableObject currentViewModel;
    }
}
