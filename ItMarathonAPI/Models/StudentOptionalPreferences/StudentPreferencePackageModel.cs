using ItMarathon.Api.Models.Courses;

namespace ItMarathon.Api.Models.StudentOptionalPreferences
{
    public class StudentPreferencePackageModel
    {
        public IEnumerable<StudentPreferenceWithCourseModel> Options { get; set; }
    }
}
