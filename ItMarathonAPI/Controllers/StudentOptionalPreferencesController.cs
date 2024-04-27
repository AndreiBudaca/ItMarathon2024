using ItMarathon.Api.Models.Courses;
using ItMarathon.Api.Models.StudentOptionalPreferences;
using ItMarathon.Core.CustomClaims;
using ItMarathon.Service.Courses;
using ItMarathon.Service.StudentOptionalPreferences;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ItMarathon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentOptionalPreferencesController : ControllerBase
    {
        private readonly IStudentOptionalPreferenceService studentOptionalPreferenceService;

        private readonly ICoursesService coursesService;

        public StudentOptionalPreferencesController(IStudentOptionalPreferenceService studentOptionalPreferenceService,
            ICoursesService coursesService)
        {
            this.studentOptionalPreferenceService = studentOptionalPreferenceService;
            this.coursesService = coursesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPreferences()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userYearOfStudy = int.Parse(User.Claims.FirstOrDefault(c => c.Type == CustomClaims.YearOfStudy)?.Value ?? "0");

            var userPreferences = await studentOptionalPreferenceService.GetAsync(userId);
            var courses = await coursesService.GetAllAsync(userYearOfStudy + 1, true);

            var packageIds = courses.Select(c => c.OptionalPackage ?? 0).ToList();

            var model = new List<StudentPreferencePackageModel>();
            foreach (var packageId in packageIds)
            {
                model.Add(new StudentPreferencePackageModel
                {
                    StudentPreferences = userPreferences.Where(up => (courses.First(c => c.Id == up.OptionalId).OptionalPackage ?? 0) == packageId)
                    .Select(up => new StudentPreferenceModel
                    {
                        OptionalId = up.OptionalId,
                        StudentId = up.OptionalId,
                        SortOrder = up.SortOrder,
                    }),
                    Options = courses.Where(c => (c.OptionalPackage ?? 0) == packageId)
                    .Select(c => new CourseModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Credits = c.Credits,
                        Semester = c.Semester,
                        IsOptional = c.IsOptional,
                        OptionalPackage = packageId
                    })
                });
            }

            return Ok(model);
        }
    }
}
