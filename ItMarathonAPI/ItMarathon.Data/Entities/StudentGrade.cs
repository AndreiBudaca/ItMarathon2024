using Microsoft.EntityFrameworkCore;

namespace ItMarathon.Data.Entities
{
    [PrimaryKey(nameof(StudentId), nameof(CourseId))]
    public class StudentGrade
    {
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public decimal Grade {  get; set; }

        public int StudyYear { get; set; }

        public User Student { get; set; }

        public Course Course { get; set; }
    }
}
