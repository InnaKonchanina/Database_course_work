using DatabaseCourceWork.DesktopApplication.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseCourceWork.DesktopApplication.Database
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<CulturalEvent> CulturalEvents { get; set; } = null!;
        public DbSet<ArtistToCulturalEventMap> ArtistToCulturalEventMap { get; set; } = null!;
        public DbSet<VisitorToCulturalEventMap> VisitorToCulturalEventMap { get; set; } = null!;
        public DbSet<Feedback> Feedbacks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Database/DbFiles/FestivalDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // Optional: enforce composite uniqueness or keys if needed
        }
    }
}
