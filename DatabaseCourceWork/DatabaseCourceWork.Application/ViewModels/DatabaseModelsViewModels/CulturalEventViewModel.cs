using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseCourceWork.DesktopApplication.Database.Models;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels
{
    internal partial class CulturalEventViewModel : ObservableObject
    {
        public CulturalEventViewModel()
        {

        }
        public CulturalEventViewModel(CulturalEvent culturalEvent)
        {
            Id = culturalEvent.Id;
            Title = culturalEvent.Title;
            Description = culturalEvent.Description;
            StartDateTime = culturalEvent.StartDateTime;
            EndDateTime = culturalEvent.EndDateTime;
            LocationId = culturalEvent.LocationId;
            Location = new LocationViewModel(culturalEvent.Location);
            OrganizerId = culturalEvent.OrganizerId;
            Organizer = new UserViewModel(culturalEvent.Organizer);
            Status = culturalEvent.Status;

            Feedbacks = culturalEvent.Feedbacks
                .Select(f => new FeedbackViewModel(f, false))
                .ToList();

            Artists = culturalEvent.Artists
                .Select(a => new UserViewModel(a))
                .ToList();

            Visitors = culturalEvent.Visitors
                .Select(v => new UserViewModel(v))
                .ToList();
        }

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
        [ObservableProperty] private List<UserViewModel> artists = new();
        [ObservableProperty] private List<UserViewModel> visitors = new();

        private const int PreviewCount = 3;

        [ObservableProperty]
        private bool areArtistsExpanded = false;

        [ObservableProperty]
        private bool areVisitorsExpanded = false;

        [ObservableProperty]
        private bool areFeedbacksExpanded = false;

        public List<UserViewModel> ArtistsPreview =>
            AreArtistsExpanded ? Artists : Artists.Take(PreviewCount).ToList();

        public List<UserViewModel> VisitorsPreview =>
            AreVisitorsExpanded ? Visitors : Visitors.Take(PreviewCount).ToList();



        public List<FeedbackViewModel> FeedbacksPreview =>
            AreFeedbacksExpanded ? Feedbacks : Feedbacks.Take(PreviewCount).ToList();

        public IRelayCommand ToggleFeedbacksCommand => new RelayCommand(() =>
        {
            AreFeedbacksExpanded = !AreFeedbacksExpanded;
            OnPropertyChanged(nameof(FeedbacksPreview));
        });

        public IRelayCommand ToggleArtistsCommand => new RelayCommand(() =>
        {
            AreArtistsExpanded = !AreArtistsExpanded;
            OnPropertyChanged(nameof(ArtistsPreview));
        });

        public IRelayCommand ToggleVisitorsCommand => new RelayCommand(() =>
        {
            AreVisitorsExpanded = !AreVisitorsExpanded;
            OnPropertyChanged(nameof(VisitorsPreview));
        });

        public CulturalEvent ToModel()
        {
            return new CulturalEvent
            {
                Id = this.Id,
                Title = this.Title ?? string.Empty,
                Description = this.Description,
                StartDateTime = this.StartDateTime,
                EndDateTime = this.EndDateTime,
                LocationId = this.Location?.Id ?? 0,
                OrganizerId = this.OrganizerId,
                Status = this.Status ?? "scheduled",
                Artists = this.Artists?.Select(a => a.ToModel()).ToList() ?? new List<User>(),
                Visitors = this.Visitors?.Select(v => v.ToModel()).ToList() ?? new List<User>(),
                Feedbacks = this.Feedbacks?.Select(f => f.ToModel()).ToList() ?? new List<Feedback>()
            };
        }
    }
}
