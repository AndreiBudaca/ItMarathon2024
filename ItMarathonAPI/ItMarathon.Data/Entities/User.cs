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

        public int YearOfStudy { get; set; }

        public int Semester { get; set; }

        public UserRole Role { get; set; }

        public ICollection<StudentGrade> Grades { get; set; }
        
        public ICollection<StudentOptionalPreference> StudentOptionalPreferences { get; set; }

    }
}
