﻿using DatabaseCourceWork.DesktopApplication.Database.Models;
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

        public List<CulturalEvent> GetAllEvents()
        {
            var events = _context.CulturalEvents
                .Include(e => e.Location)
                .Include(e => e.Organizer)
                .ToList();

            foreach (var e in events)
            {
                e.Artists.Clear();
                e.Visitors.Clear();
                e.Feedbacks.Clear();
                e.Artists.AddRange(GetArtistsByEventId(e.Id));
                e.Visitors.AddRange(GetVisitorsByEventId(e.Id));
                e.Feedbacks.AddRange(GetFeedbacksByEventId(e.Id));


            }
            return events;
        }

        public List<User> GetArtistsByEventId(int eventId)
            => _context.ArtistToCulturalEventMap
                .Where(a => a.CulturalEventId == eventId)
                .Include(a => a.Artist)
                .Select(a => a.Artist)
                .ToList();

        public List<User> GetVisitorsByEventId(int eventId)
            => _context.VisitorToCulturalEventMap
                .Where(v => v.CulturalEventId == eventId)
                .Include(v => v.User)
                .Select(v => v.User)
                .ToList();

        public List<Feedback> GetFeedbacksByEventId(int eventId)
            => _context.Feedbacks
                .Where(f => f.CulturalEventId == eventId)
                .Include(f => f.User)
                .Include(f => f.CulturalEvent)
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

        internal IEnumerable<Location> GetAllLocations()
        {
            return _context.Locations.ToList();
        }

        internal IEnumerable<User> GetAllArtist()
        {
            return GetAllUsers().Where(u => u.UserRole == UserRole.Artist);
        }

        internal void AddEvent(CulturalEvent culturalEvent)
        {
            _context.CulturalEvents.Add(culturalEvent);
            _context.SaveChanges();

            foreach (var artist in culturalEvent.Artists)
            {
                _context.ArtistToCulturalEventMap.Add(new ArtistToCulturalEventMap
                {
                    ArtistId = artist.Id,
                    CulturalEventId = culturalEvent.Id
                });
            }

            foreach (var artist in culturalEvent.Visitors)
            {
                _context.VisitorToCulturalEventMap.Add(new VisitorToCulturalEventMap
                {
                    UserId = artist.Id,
                    CulturalEventId = culturalEvent.Id,
                    RegisteredAt = DateTime.Now,
                });
            }

            _context.SaveChanges();
        }

        internal int GetUsingCountForLocation(int id)
        {
            return GetAllEvents().Count(e => e.LocationId == id);
        }

        internal void JoinEvent(int userId, int eventId)
        {
            _context.VisitorToCulturalEventMap.Add(new VisitorToCulturalEventMap
            {
                UserId = userId,
                CulturalEventId = eventId,
                RegisteredAt = DateTime.Now
            });
            _context.SaveChanges();
        }

        internal void LeaveFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }

        internal void AddLocation(Location location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
        }

        internal void SaveUser(User user)
        {
            var existedUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existedUser == null)
            {
                throw new InvalidOperationException($"User with Id {user.Id} not found.");
            }

            existedUser.Experience = user.Experience;
            existedUser.CreativeInterests = user.CreativeInterests;
            existedUser.Photo = user.Photo;

            _context.SaveChanges();
        }
    }
}
