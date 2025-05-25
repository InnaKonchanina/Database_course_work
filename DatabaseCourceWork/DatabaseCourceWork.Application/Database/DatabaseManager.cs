using DatabaseCourceWork.DesktopApplication.Database.Models;
using DatabaseCourceWork.DesktopApplication.Database.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DatabaseCourceWork.DesktopApplication.Database
{
    internal class DatabaseManager
    {
        private static readonly Lazy<DatabaseManager> _instance = new(() => new DatabaseManager());
        public static DatabaseManager Instance => _instance.Value;

        private readonly DatabaseContext _context;

        private DatabaseManager()
        {
            _context = new DatabaseContext();
            _context.Database.EnsureCreated();
        }

        public List<User> GetAllUsers()
            => _context.Users.ToList();

        public List<User> GetUsersByRole(UserRole role)
            => _context.Users
                .Where(u => u.Role == role.ToString())
                .ToList();

        public List<CulturalEvent> GetAllEvents()
            => _context.CulturalEvents
                .Include(e => e.Location)
                .Include(e => e.Organizer)
                .ToList();

        public CulturalEvent? GetEventById(int eventId)
            => _context.CulturalEvents
                .Include(e => e.Location)
                .Include(e => e.Organizer)
                .FirstOrDefault(e => e.Id == eventId);

        public List<User> GetArtistsByEventId(int eventId)
            => _context.ArtistToCulturalEventMaps
                .Where(a => a.CulturalEventId == eventId)
                .Include(a => a.Artist)
                .Select(a => a.Artist)
                .ToList();

        public List<User> GetVisitorsByEventId(int eventId)
            => _context.VisitorToCulturalEventMaps
                .Where(v => v.CulturalEventId == eventId)
                .Include(v => v.User)
                .Select(v => v.User)
                .ToList();

        public void SaveChanges()
            => _context.SaveChanges();

        internal void RegisterNewUser(User user)
        {
            user.Role = user.Role.ToLower();
            _context.Users.Add(user);
            SaveChanges();
        }

        internal bool CheckIfEmailIsUsed(string email)
        {
            return GetAllUsers().Any(u => u.Email == email);
        }

        internal User? TryLogin(string email, string password)
        {
            return GetAllUsers().FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
        }
    }
}
