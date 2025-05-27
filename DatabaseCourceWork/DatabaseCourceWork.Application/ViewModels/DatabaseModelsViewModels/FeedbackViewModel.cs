using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseCourceWork.DesktopApplication.Database.Models;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels
{
    internal partial class FeedbackViewModel : ObservableObject
    {
        public FeedbackViewModel(Feedback feedback, bool loadEvent)
        {
            Id = feedback.Id;
            CulturalEventId = feedback.CulturalEventId;
            if (loadEvent) 
            {
                CulturalEvent = new CulturalEventViewModel(feedback.CulturalEvent);
            }
            
            UserId = feedback.UserId;
            User = new UserViewModel(feedback.User);
            Rating = feedback.Rating;
            Comment = feedback.Comment;
        }

        [ObservableProperty] private int id;
        [ObservableProperty] private int culturalEventId;
        [ObservableProperty] private int userId;
        [ObservableProperty] private int rating;
        [ObservableProperty] private string? comment;
        [ObservableProperty] private UserViewModel user;
        [ObservableProperty] private CulturalEventViewModel culturalEvent;

        public Feedback ToModel()
        {
            return new Feedback
            {
                Id = this.Id,
                CulturalEventId = this.CulturalEventId,
                CulturalEvent = this.CulturalEvent?.ToModel() ?? new CulturalEvent(),
                UserId = this.UserId,
                User = this.User?.ToModel() ?? new User(),
                Rating = this.Rating,
                Comment = this.Comment
            };
        }
    }
}
