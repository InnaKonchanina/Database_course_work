using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using DatabaseCourceWork.DesktopApplication.Database;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels
{
    internal partial class LocationViewModel : ObservableObject
    {
        public LocationViewModel(Location location)
        {
            Id = location.Id;
            Name = location.Name ?? string.Empty;
            Description = location.Description;
            Address = location.Address;
            Capacity = location.Capacity ?? 0;
            Photo = location.Photo;

            if (Photo != null)
            {
                PhotoImage = LoadImage(Photo);
            }

            UsingCount = DatabaseManager.Instance.GetUsingCountForLocation(Id);
        }

        [ObservableProperty] private int id;
        [ObservableProperty] private string name;
        [ObservableProperty] private string? description;
        [ObservableProperty] private string? address;
        [ObservableProperty] private int capacity;
        [ObservableProperty] private byte[]? photo;
        [ObservableProperty] private int usingCount;

        private ImageSource? photoImage;
        public ImageSource? PhotoImage
        {
            get => photoImage;
            private set => SetProperty(ref photoImage, value);
        }

        partial void OnPhotoChanged(byte[]? value)
        {
            PhotoImage = value != null ? LoadImage(value) : null;
        }

        private ImageSource? LoadImage(byte[] imageData)
        {
            try
            {
                using (var ms = new MemoryStream(imageData))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    image.Freeze();
                    return image;
                }

            }
            catch
            {
                return null;
            }
        }

        public Location ToModel()
        {
            return new Location
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Address = this.Address,
                Capacity = this.Capacity == 0 ? (int?)null : this.Capacity,
                Photo = this.Photo
                // CulturalEvents are not included here, usually handled separately
            };
        }
    }
}
