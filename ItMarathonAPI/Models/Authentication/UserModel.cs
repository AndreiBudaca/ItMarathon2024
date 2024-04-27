using System.ComponentModel.DataAnnotations;

namespace ItMarathon.Api.Models.Authentication
{
    public class UserModel
    {
        [Required]
        [EmailAddress (ErrorMessage = "Please provide a valid email address")]
        public string Email { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "First name must have less then 50 characters")]
        public string FirstName {  get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Last name must have less then 50 characters")]
        public string LastName {  get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(50, ErrorMessage = "Password must have less then 50 characters")]
        public string Password { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Please provide a valid value for the year of study")]
        public int YearOfStudy {  get; set; }
    }
}
