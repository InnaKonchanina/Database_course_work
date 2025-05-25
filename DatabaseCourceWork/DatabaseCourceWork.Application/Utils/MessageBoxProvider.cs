namespace DatabaseCourceWork.DesktopApplication.Utils
{
    using System.Windows;

    namespace DatabaseCourceWork.DesktopApplication.Services
    {
        internal class MessageBoxProvider
        {
            // Static lazy instance for thread-safe singleton
            private static readonly Lazy<MessageBoxProvider> _instance = new(() => new MessageBoxProvider());

            public static MessageBoxProvider Instance => _instance.Value;

            // Private constructor to prevent external instantiation
            private MessageBoxProvider() { }

            // Wrapper methods for showing message boxes
            public void Show(string message, string caption = "Info", MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Information)
            {
                MessageBox.Show(message, caption, buttons, icon);
            }

            public bool ShowConfirmation(string message, string caption = "Confirm")
            {
                var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
                return result == MessageBoxResult.Yes;
            }

            public void ShowError(string message, string caption = "Error")
            {
                MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            public void ShowWarning(string message, string caption = "Warning")
            {
                MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

}
