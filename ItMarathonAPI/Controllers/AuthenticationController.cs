using ItMarathon.Api.Models.Authentication;
using ItMarathon.Core;
using ItMarathon.Service.Authentication;
using ItMarathon.Service.Authentication.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ItMarathon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserModel login)
        {
            var user = await authenticationService.LoginAsync(new LoginUserDto
            {
                Email = login.Email,
                Password = login.Password
            });

            if (user == null) return Unauthorized("The email / password combination is invalid");

            var token = authenticationService.GenerateJSONWebToken(user);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage);
            }

            var error = await authenticationService.RegisterAsync(new CreateUserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                YearOfStudy = user.YearOfStudy,
                Semester = DateTime.Now.Month >= 2 && DateTime.Now.Month <= 9 ? 2 : 1,
                Role = Core.Types.UserRole.Student
            });

            if (String.IsNullOrEmpty(error))
            {
                return Ok();
            }

            return UnprocessableEntity(error);
        }
    }
}
