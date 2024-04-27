using Microsoft.EntityFrameworkCore;

namespace ItMarathon.Data.Entities
{
    [PrimaryKey(nameof(StudentId), nameof(OptionalId))]
    public class StudentOptionalPreference
    {
        public int StudentId { get; set; }

        public int OptionalId { get; set; }

        public int SortOrder { get; set; }

        public User Student { get; set; }

        public Course Optional { get; set; }
    }
}
