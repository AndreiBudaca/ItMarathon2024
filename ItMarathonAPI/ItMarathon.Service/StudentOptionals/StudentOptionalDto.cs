using ItMarathon.Service.Authentication.Dto;
using ItMarathon.Service.Courses.Dto;

namespace ItMarathon.Service.StudentOptionals
{
    public class StudentOptionalDto
    {
        public UserDto Student { get; set; }

        public CourseDto Course { get; set; }
    }
}
