using DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
