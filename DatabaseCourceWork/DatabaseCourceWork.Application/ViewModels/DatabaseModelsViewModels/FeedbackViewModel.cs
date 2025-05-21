using CommunityToolkit.Mvvm.ComponentModel;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels
{
    internal partial class FeedbackViewModel : ObservableObject
    {
        [ObservableProperty] private int id;
        [ObservableProperty] private int culturalEventId;
        [ObservableProperty] private int userId;
        [ObservableProperty] private int rating;
        [ObservableProperty] private string? comment;
        [ObservableProperty] private UserViewModel user;
        [ObservableProperty] private CulturalEventViewModel culturalEvent;
    }
}
