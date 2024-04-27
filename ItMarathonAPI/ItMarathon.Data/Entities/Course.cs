using Microsoft.EntityFrameworkCore;

namespace ItMarathon.Data.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int YearOfStudy { get; set; }

        public int Credits { get; set; }

        public int Semester { get; set; }

        public bool IsOptional { get; set; }

        public int? OptionalPackage { get; set; }

        public ICollection<StudentGrade> Grades { get; set; }

        public ICollection<StudentOptionalPreference> StudentOptionalPreferences { get; set; }
    }
}
