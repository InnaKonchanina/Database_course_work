namespace DatabaseCourceWork.DesktopApplication.Database.Models
{
    internal class Location
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public int? Capacity { get; set; }
        public byte[]? Photo { get; set; }

        public ICollection<CulturalEvent> CulturalEvents { get; set; } = new List<CulturalEvent>();
    }
}
