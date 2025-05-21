namespace DatabaseCourceWork.DesktopApplication.Database.Models
{
    internal class ArtistToCulturalEventMap
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public User Artist { get; set; } = null!;
        public int CulturalEventId { get; set; }
        public CulturalEvent CulturalEvent { get; set; } = null!;
    }
}
