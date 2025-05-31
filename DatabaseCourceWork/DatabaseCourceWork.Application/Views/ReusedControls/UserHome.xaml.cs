using DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DatabaseCourceWork.DesktopApplication.Views.ReusedControls
{
    /// <summary>
    /// Interaction logic for UserHome.xaml
    /// </summary>
    public partial class UserHome : UserControl
    {
        public UserHome()
        {
            InitializeComponent();
        }

        private void ChangePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserViewModel vm)
            {
                var openFileDialog = new OpenFileDialog
                {
                    Title = "Select Photo",
                    Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        var bytes = File.ReadAllBytes(openFileDialog.FileName);
                        vm.Photo = bytes; // триггерит OnPhotoChanged, обновляет PhotoImage
                    }
                    catch
                    {
                        MessageBox.Show("Failed to load image.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
