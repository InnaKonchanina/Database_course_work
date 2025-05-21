using DatabaseCourceWork.DesktopApplication.Database.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCourceWork.DesktopApplication.Database.Models
{
    internal class CulturalEvent
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; } = null!;

        public int OrganizerId { get; set; }
        public User Organizer { get; set; } = null!;

        public string Status { get; set; } = "scheduled";

        public ICollection<ArtistToCulturalEventMap> Artists { get; set; } = new List<ArtistToCulturalEventMap>();
        public ICollection<VisitorToCulturalEventMap> Visitors { get; set; } = new List<VisitorToCulturalEventMap>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
