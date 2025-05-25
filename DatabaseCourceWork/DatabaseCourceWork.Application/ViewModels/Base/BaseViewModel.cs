using CommunityToolkit.Mvvm.ComponentModel;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.Base
{
    internal class BaseViewModel : ObservableValidator
    {
        protected readonly MainWindowViewModel _mainWindowViewModel;

        protected BaseViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this._mainWindowViewModel = mainWindowViewModel;
        }

        public void ValidateAll()
        {
            ValidateAllProperties();
        }

        protected string GetFirstError(string propertyName)
        {
            if (GetErrors(propertyName) is IEnumerable<string> errors)
            {
                return errors.FirstOrDefault();
            }
            return null;
        }
    }
}
