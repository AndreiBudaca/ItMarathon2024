using ItMarathon.Api.Models.Courses;

namespace ItMarathon.Api.Models.StudentOptionalPreferences
{
    public class StudentPreferenceWithCourseModel : CourseModel
    {
        public int? SortOrder { get; set; }
    }
}
