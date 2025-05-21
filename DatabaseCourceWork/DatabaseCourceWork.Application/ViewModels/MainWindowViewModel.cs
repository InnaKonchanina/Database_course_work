using CommunityToolkit.Mvvm.ComponentModel;

namespace DatabaseCourceWork.DesktopApplication.ViewModels
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel()
        {
            Title = "Hello MVVM!";
        }

        [ObservableProperty]
        private string title;
    }
}
