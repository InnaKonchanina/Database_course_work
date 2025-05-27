using DatabaseCourceWork.DesktopApplication.Database.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public List<User> Artists { get; set; } = new List<User>();
        [NotMapped]
        public List<User> Visitors { get; set; } = new List<User>();
        [NotMapped]
        public List<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
