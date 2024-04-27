using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ItMarathon.Api.Models.StudentOptionalPreferences
{
    public class StudentPreferenceModel
    {
        public int StudentId { get; set; }
     
        public int OptionalId { get; set; }

        public int SortOrder { get; set; }
    }
}
