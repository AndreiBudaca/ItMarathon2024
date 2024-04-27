using ItMarathon.Core.Types;

namespace ItMarathon.Service.Authentication.Dto
{
    public class CreateUserDto : UserDto
    {
        public string Password { get; set; }

    }
}
