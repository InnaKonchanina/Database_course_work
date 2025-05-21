using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels
{
    internal partial class LocationViewModel : ObservableObject
    {
        [ObservableProperty] private int id;
        [ObservableProperty] private string name;
        [ObservableProperty] private string? description;
        [ObservableProperty] private string? address;
        [ObservableProperty] private int capacity;
        [ObservableProperty] private byte[]? photo;
    }
}
