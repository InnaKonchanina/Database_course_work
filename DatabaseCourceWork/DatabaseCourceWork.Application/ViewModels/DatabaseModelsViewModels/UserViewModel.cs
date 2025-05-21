using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseCourceWork.DesktopApplication.Database.Models;

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

        public UserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            PasswordHash = user.PasswordHash;
            Role = user.Role;
            Experience = user.Experience;
            CreativeInterests = user.CreativeInterests;
        }

        public User ToModel() => new()
        {
            Id = this.Id,
            Name = this.Name,
            Email = this.Email,
            PasswordHash = this.PasswordHash,
            Role = this.Role,
            Experience = this.Experience,
            CreativeInterests = this.CreativeInterests
        };
    }
}
