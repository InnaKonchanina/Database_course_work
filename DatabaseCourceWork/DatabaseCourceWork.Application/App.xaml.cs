using DatabaseCourceWork.DesktopApplication.Database;
using System.Windows;

namespace DatabaseCourceWork.DesktopApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    internal partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DatabaseManager databaseManager = new DatabaseManager();
        }
    }

}
