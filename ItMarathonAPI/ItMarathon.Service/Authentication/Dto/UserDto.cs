using ItMarathon.Core.Types;

namespace ItMarathon.Service.Authentication.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int YearOfStudy {  get; set; }

        public UserRole Role { get; set; }
    }
}
