namespace DatabaseCourceWork.DesktopApplication.Database.Models
{
    internal class VisitorToCulturalEventMap
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int CulturalEventId { get; set; }
        public CulturalEvent CulturalEvent { get; set; } = null!;
        public DateTime RegisteredAt { get; set; }
    }
}
