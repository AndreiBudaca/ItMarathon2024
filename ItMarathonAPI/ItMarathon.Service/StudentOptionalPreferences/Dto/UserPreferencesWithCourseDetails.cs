using ItMarathon.Service.Courses.Dto;

namespace ItMarathon.Service.StudentOptionalPreferences.Dto
{
    public class UserPreferencesWithCourseDetails
    {
        public CourseDto Course { get; set; }

        public int SortOrder { get; set; }
    }
}
