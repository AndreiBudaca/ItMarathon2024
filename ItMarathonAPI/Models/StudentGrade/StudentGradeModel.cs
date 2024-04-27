using System.ComponentModel.DataAnnotations;

namespace ItMarathon.Api.Models.StudentGrade
{
    public class StudentGradeModel
    {
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        [Range(0, 10, ErrorMessage = "Please provide a valid value for grade")]
        public decimal Grade { get; set; }
    }
}
