using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.Database.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DatabaseCourceWork.DesktopApplication.Database
{
    internal class DatabaseManager
    {
        private readonly DatabaseContext _context;

        public DatabaseManager()
        {
            _context = new DatabaseContext();
            _context.Database.EnsureCreated();
        }

        public async Task<List<User>> GetAllUsersAsync()
            => await _context.Users.ToListAsync();

        public async Task<List<User>> GetUsersByRoleAsync(UserRole role)
            => await _context.Users
                .Where(u => u.Role == role.ToString())
                .ToListAsync();

        public async Task<List<CulturalEvent>> GetAllEventsAsync()
            => await _context.CulturalEvents
                .Include(e => e.Location)
                .Include(e => e.Organizer)
                .ToListAsync();

        public async Task<CulturalEvent> GetEventByIdAsync(int eventId)
            => await _context.CulturalEvents
                .Include(e => e.Location)
                .Include(e => e.Organizer)
                .FirstOrDefaultAsync(e => e.Id == eventId);

        public async Task<List<User>> GetArtistsByEventIdAsync(int eventId)
            => await _context.ArtistToCulturalEventMaps
                .Where(a => a.CulturalEventId == eventId)
                .Include(a => a.Artist)
                .Select(a => a.Artist)
                .ToListAsync();

        public async Task<List<User>> GetVisitorsByEventIdAsync(int eventId)
            => await _context.VisitorToCulturalEventMaps
                .Where(v => v.CulturalEventId == eventId)
                .Include(v => v.User)
                .Select(v => v.User)
                .ToListAsync();

        // Add more specific queries as needed...

        // Optional: method to save changes
        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
