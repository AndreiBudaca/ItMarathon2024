using System.ComponentModel.DataAnnotations;

namespace ItMarathon.Api.Models.Courses
{
    public class CourseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The course name should be less than 255 characters")]
        public string Name { get; set; }

        [MaxLength(1024, ErrorMessage = "The course description should be less than 1024 characters")]
        public string Description { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Please provide a valid value for the year of study")]
        public int YearOfStudy { get; set; }

        [Range(1, 10, ErrorMessage = "Please provide a valid value for the credits")]
        public int Credits { get; set; }

        [Range(1, 2, ErrorMessage = "Please provide a valid value for the semester")]
        public int Semester { get; set; }

        public bool IsOptional { get; set; }

        public int? OptionalPackage { get; set; }
    }
}
