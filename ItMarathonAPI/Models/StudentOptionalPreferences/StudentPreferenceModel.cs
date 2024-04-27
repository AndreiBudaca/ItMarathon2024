using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;

namespace ItMarathon.Api.Models.StudentOptionalPreferences
{
    public class StudentPreferenceModel
    {
        public int StudentId { get; set; }

        [Required]
        public int OptionalId { get; set; }

        [Required]
        public int SortOrder { get; set; }
    }
}
