using ItMarathon.Service.StudentOptionals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ItMarathon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentOptionalsController : ControllerBase
    {
        private readonly IStudentOptionalsService studentOptionalsService;

        public StudentOptionalsController(IStudentOptionalsService studentOptionalsService)
        {
            this.studentOptionalsService = studentOptionalsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNextYearOptionals()
        {
            var yearOfStudy = DateTime.Now.Month >= 10 ? DateTime.Now.Year + 2 : DateTime.Now.Year + 1;
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            var optionals = await studentOptionalsService.GetOptionals(yearOfStudy, userId);

            return Ok(optionals);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOptionals()
        {
            var yearOfStudy = DateTime.Now.Month >= 10 ? DateTime.Now.Year + 2 : DateTime.Now.Year + 1;

            await studentOptionalsService.RemoveOptionals(yearOfStudy);

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DistributeOptionals()
        {
            var yearOfStudy = DateTime.Now.Month >= 10 ? DateTime.Now.Year + 2 : DateTime.Now.Year + 1;
            
            await studentOptionalsService.DistributeStudents(yearOfStudy);
            
            return NoContent();
        }
    }
}
