using DatabaseCourceWork.DesktopApplication.Database.Models.Enums;

namespace DatabaseCourceWork.DesktopApplication.Database.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Experience { get; set; }
        public string? CreativeInterests { get; set; }

        public byte[]? Photo { get; set; }

        public UserRole UserRole
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Role))
                    throw new InvalidOperationException("Role is null or empty.");

                // Capitalize first letter, lowercase the rest
                var formattedRole = char.ToUpper(Role[0]) + Role.Substring(1).ToLower();

                return Enum.Parse<UserRole>(formattedRole);
            }
        }
    }
}
