using CommunityToolkit.Mvvm.ComponentModel;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels
{
    internal partial class CulturalEventViewModel : ObservableObject
    {
        [ObservableProperty] private int id;
        [ObservableProperty] private string title;
        [ObservableProperty] private string? description;
        [ObservableProperty] private DateTime startDateTime;
        [ObservableProperty] private DateTime endDateTime;
        [ObservableProperty] private int locationId;
        [ObservableProperty] private LocationViewModel location;
        [ObservableProperty] private int organizerId;
        [ObservableProperty] private UserViewModel organizer;
        [ObservableProperty] private string status;
        [ObservableProperty] private List<FeedbackViewModel> feedbacks = new();
    }
}
