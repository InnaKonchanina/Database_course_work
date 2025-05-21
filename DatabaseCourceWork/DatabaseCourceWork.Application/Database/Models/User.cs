using DatabaseCourceWork.DesktopApplication.Database.Models.Enums;

namespace DatabaseCourceWork.DesktopApplication.Database.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Experience { get; set; }
        public string? CreativeInterests { get; set; }

        public ICollection<ArtistToCulturalEventMap> ArtistEvents { get; set; } = new List<ArtistToCulturalEventMap>();
        public ICollection<VisitorToCulturalEventMap> VisitorEvents { get; set; } = new List<VisitorToCulturalEventMap>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public ICollection<CulturalEvent> OrganizedEvents { get; set; } = new List<CulturalEvent>();
    }
}
