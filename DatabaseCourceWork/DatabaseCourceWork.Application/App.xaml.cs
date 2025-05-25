using DatabaseCourceWork.DesktopApplication.Utils.DatabaseCourceWork.DesktopApplication.Services;
using System.Windows;
using System.Windows.Threading;

namespace DatabaseCourceWork.DesktopApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    internal partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {

        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBoxProvider.Instance.ShowError($"Unexpected error. Message is {e.Exception.Message}.");
            e.Handled = true;
        }
    }

}
