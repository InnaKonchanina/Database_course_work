using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels
{
    internal partial class UserViewModel : ObservableObject
    {
        [ObservableProperty] private int id;
        [ObservableProperty] private string name;
        [ObservableProperty] private string email;
        [ObservableProperty] private string passwordHash;
        [ObservableProperty] private string role;
        [ObservableProperty] private string? experience;
        [ObservableProperty] private string? creativeInterests;
        [ObservableProperty] private byte[]? photo;

        private bool isInit;

        public UserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            PasswordHash = user.PasswordHash;
            Role = user.Role;
            Experience = user.Experience;
            CreativeInterests = user.CreativeInterests;

            Photo = user.Photo;

            if (Photo != null)
            {
                PhotoImage = LoadImage(Photo);
            }

            isInit = true;
        }

        private ImageSource? photoImage;
        public ImageSource? PhotoImage
        {
            get => photoImage;
            private set => SetProperty(ref photoImage, value);
        }

        partial void OnPhotoChanged(byte[]? value)
        {
            PhotoImage = value != null ? LoadImage(value) : null;
            if (isInit)
                DatabaseManager.Instance.SaveUser(ToModel());
        }

        [ObservableProperty]
        private bool isModified = false;

        partial void OnExperienceChanged(string? value)
        {
            if (isInit)
                DatabaseManager.Instance.SaveUser(ToModel());
        }

        partial void OnCreativeInterestsChanged(string? value)
        {
            if (isInit)
                DatabaseManager.Instance.SaveUser(ToModel());
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

        public User ToModel() => new()
        {
            Id = this.Id,
            Name = this.Name,
            Email = this.Email,
            PasswordHash = this.PasswordHash,
            Role = this.Role,
            Experience = this.Experience,
            CreativeInterests = this.CreativeInterests,
            Photo = this.Photo
        };
    }
}
