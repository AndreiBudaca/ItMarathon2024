using ItMarathon.Api.Models.StudentGrade;
using ItMarathon.Service.StudentGrades;
using ItMarathon.Service.StudentGrades.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItMarathon.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGradeController : ControllerBase
    {
        private readonly IStudentGradesService studentGradesService;

        public StudentGradeController(IStudentGradesService studentGradesService)
        {
            this.studentGradesService = studentGradesService;
        }

        [HttpPost]
        public async Task<IActionResult> AddGrade(StudentGradeModel studentGrade)
        {
            await studentGradesService.AddStudentGradeAsync(new StudentGradeDto
            {
                StudentId = studentGrade.StudentId,
                CourseId = studentGrade.CourseId,
                Grade = studentGrade.Grade,
                StudyYear = DateTime.Now.Month >= 10 ? DateTime.Now.Year + 1 : DateTime.Now.Year,
            });

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGrade(StudentGradeModel studentGrade)
        {
            await studentGradesService.UpdateStudentGradeAsync(new StudentGradeDto
            {
                StudentId = studentGrade.StudentId,
                CourseId = studentGrade.CourseId,
                Grade = studentGrade.Grade,
                StudyYear = studentGrade.StudyYear,
            });

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGrade(StudentGradeModel studentGrade)
        {
            await studentGradesService.DeleteStudentGradeAsync(new StudentGradeDto
            {
                StudentId = studentGrade.StudentId,
                CourseId = studentGrade.CourseId,
                Grade = studentGrade.Grade,
            });

            return Ok();
        }
    }
}
