using ItMarathon.Api.Models.Courses;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;

namespace ItMarathon.Api.Models.StudentOptionalPreferences
{
    public class StudentPreferenceModel
    {
        [Required]
        public int OptionalId { get; set; }

        [Required]
        public int SortOrder { get; set; }
    }
}
