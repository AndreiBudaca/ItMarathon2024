using System.ComponentModel.DataAnnotations;

namespace ItMarathon.Api.Models.Authentication
{
    public class LoginUserModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Invalid password")]
        [MaxLength(50, ErrorMessage = "Invalid password")]
        public string Password { get; set; }
    }
}
