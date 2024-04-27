using ItMarathon.Core.Types;
using Microsoft.EntityFrameworkCore;

namespace ItMarathon.Data.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] PasswordHash { get; set; }

        public UserRole Role { get; set; }
    }
}
