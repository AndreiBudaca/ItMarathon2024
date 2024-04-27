using ItMarathon.Api.Models.Courses;
using ItMarathon.Service.Courses;
using ItMarathon.Service.Courses.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItMarathon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await coursesService.GetAllAsync();

            return Ok(courses.Select(course => new CourseModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                YearOfStudy = course.YearOfStudy,
                Semester = course.Semester,
                IsOptional = course.IsOptional,
                Credits = course.Credits,
                OptionalPackage = course.OptionalPackage,
            }));
        }

        [HttpGet("[controller]/{id}")]
        public async Task<IActionResult> GetCourse([FromRoute] int id)
        {
            var course = await coursesService.GetAsync(id);

            if (course == null) return NotFound();

            return Ok(new CourseModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                YearOfStudy = course.YearOfStudy,
                Semester = course.Semester,
                IsOptional = course.IsOptional,
                Credits = course.Credits,
                OptionalPackage = course.OptionalPackage,
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost()]
        public async Task<IActionResult> AddCourse([FromBody] CourseModel course)
        {
            if (course.IsOptional && !course.OptionalPackage.HasValue)
                return UnprocessableEntity("Optional courses must have a package");

            await coursesService.AddAsync(new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                YearOfStudy = course.YearOfStudy,
                Semester = course.Semester,
                IsOptional = course.IsOptional,
                Credits = course.Credits,
                OptionalPackage = course.OptionalPackage,
            });

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut()]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseModel course)
        {
            if (course.IsOptional && !course.OptionalPackage.HasValue)
                return UnprocessableEntity("Optional courses must have a package");

            await coursesService.UpdateAsync(new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                YearOfStudy = course.YearOfStudy,
                Semester = course.Semester,
                IsOptional = course.IsOptional,
                Credits = course.Credits,
                OptionalPackage = course.OptionalPackage,
            });

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("[controller]/{id}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int id)
        {
            await coursesService.DeleteAsync(id);

            return Ok();
        }
    }
}
