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

        public UserRole UserRole
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Role))
                    throw new InvalidOperationException("Role is null or empty.");

                // Capitalize first letter, lowercase the rest
                var formattedRole = char.ToUpper(Role[0]) + Role.Substring(1).ToLower();

                return Enum.Parse<UserRole>(formattedRole);
            }
        }

        public ICollection<ArtistToCulturalEventMap> ArtistEvents { get; set; } = new List<ArtistToCulturalEventMap>();
        public ICollection<VisitorToCulturalEventMap> VisitorEvents { get; set; } = new List<VisitorToCulturalEventMap>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public ICollection<CulturalEvent> OrganizedEvents { get; set; } = new List<CulturalEvent>();
    }
}
