namespace DatabaseCourceWork.DesktopApplication.Database.Models
{
    internal class Feedback
    {
        public int Id { get; set; }
        public int CulturalEventId { get; set; }
        public CulturalEvent CulturalEvent { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
