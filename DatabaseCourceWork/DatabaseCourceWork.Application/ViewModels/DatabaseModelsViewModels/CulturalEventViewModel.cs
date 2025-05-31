using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseCourceWork.DesktopApplication.Database;
using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.Utils.DatabaseCourceWork.DesktopApplication.Services;
using DatabaseCourceWork.DesktopApplication.ViewModels.Reused;
using DatabaseCourceWork.DesktopApplication.ViewModels.VisitorViewModels;
using DatabaseCourceWork.DesktopApplication.Views.ReusedControls;
using System.Windows;

namespace DatabaseCourceWork.DesktopApplication.ViewModels.DatabaseModelsViewModels
{
    internal partial class CulturalEventViewModel : ObservableObject
    {
        private readonly UserViewModel _activeUser;
        private readonly Action _refresh;

        public CulturalEventViewModel()
        {

        }

        public CulturalEventViewModel(
            UserViewModel activeUser,
            CulturalEvent culturalEvent,
            Action refresh,
            bool allowLeaveFeedback = false,
            bool allowJoinEvent = false)
        {
            _activeUser = activeUser;
            _refresh = refresh;
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
            Price = culturalEvent.Price;

            Feedbacks = culturalEvent.Feedbacks
                .Select(f => new FeedbackViewModel(f, false))
                .ToList();

            Artists = culturalEvent.Artists
                .Select(a => new UserViewModel(a))
                .ToList();

            Visitors = culturalEvent.Visitors
                .Select(v => new UserViewModel(v))
                .ToList();

            if (IsUpcoming && allowJoinEvent && Visitors.All(v => v.Id != _activeUser.Id))
            {
                JoinEventButtonVisibility = Visibility.Visible;
            }

            if (!IsUpcoming && allowLeaveFeedback && (Visitors.Any(v => v.Id == _activeUser.Id) || Artists.Any(a => a.Id == _activeUser.Id)))
            {
                LeaveFeedbackButtonVisibility = Visibility.Visible;
            }
        }

        public Visibility LeaveFeedbackButtonVisibility { get; } = Visibility.Collapsed;

        public Visibility JoinEventButtonVisibility { get; } = Visibility.Collapsed;

        public bool IsUpcoming => EndDateTime >= DateTime.Now;
        public int VisitorsCount => Visitors.Count;

        public bool IsEventJoined => Visitors.Any(v => _activeUser.Id == v.Id);

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
        [ObservableProperty] private int price;
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

        [RelayCommand]
        private void LeaveFeedback()
        {
            if (new LeaveFeedbackDialog()
            {
                DataContext = new LeaveFeedbackViewModel(_activeUser, this)
            }.ShowDialog() == true)
            {
                _refresh();
            }
        }

        [RelayCommand]
        private void JoinEvent()
        {
            var viewModel = new PaymentViewModel(Title, Price);
            var dialog = new PaymentDialog { DataContext = viewModel };
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                DatabaseManager.Instance.JoinEvent(_activeUser.Id, Id);
                _refresh();
                MessageBoxProvider.Instance.Show($"🎉 You’ve successfully joined the event! We’ve saved your spot, {_activeUser.Name}. Don’t forget to bring your good vibes! 😊");
            }


        }

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
                Price = this.Price,
                Status = this.Status ?? "scheduled",
                Artists = this.Artists?.Select(a => a.ToModel()).ToList() ?? new List<User>(),
                Visitors = this.Visitors?.Select(v => v.ToModel()).ToList() ?? new List<User>(),
                Feedbacks = this.Feedbacks?.Select(f => f.ToModel()).ToList() ?? new List<Feedback>()
            };
        }
    }
}
