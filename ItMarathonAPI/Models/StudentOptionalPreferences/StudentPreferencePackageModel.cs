using ItMarathon.Api.Models.Courses;

namespace ItMarathon.Api.Models.StudentOptionalPreferences
{
    public class StudentPreferencePackageModel
    {
        public IEnumerable<StudentPreferenceModel> StudentPreferences { get; set; }

        public IEnumerable<CourseModel> Options { get; set; }
    }
}
