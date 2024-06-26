﻿using ItMarathon.Api.Models.Courses;
using ItMarathon.Api.Models.StudentOptionalPreferences;
using ItMarathon.Core.CustomClaims;
using ItMarathon.Service.Courses;
using ItMarathon.Service.StudentOptionalPreferences;
using ItMarathon.Service.StudentOptionalPreferences.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            var packageIds = courses.Select(c => c.OptionalPackage ?? 0).Distinct();

            var model = new List<StudentPreferencePackageModel>();
            foreach (var packageId in packageIds)
            {
                model.Add(new StudentPreferencePackageModel
                {
                    Options = courses.Where(c => (c.OptionalPackage ?? 0) == packageId)
                    .Select(c => new StudentPreferenceWithCourseModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Credits = c.Credits,
                        Semester = c.Semester,
                        IsOptional = c.IsOptional,
                        OptionalPackage = packageId,
                        SortOrder = userPreferences.FirstOrDefault(up => up.OptionalId == c.Id)?.SortOrder ?? null 
                    })
                });
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePreferences([FromBody] IEnumerable<StudentPreferenceModel> preferences)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userYearOfStudy = int.Parse(User.Claims.FirstOrDefault(c => c.Type == CustomClaims.YearOfStudy)?.Value ?? "0");

            if (preferences.Select(p => p.OptionalId).Distinct().Count() != preferences.Count())
            {
                return UnprocessableEntity("Please do not provide duplicate courses");
            }

            await studentOptionalPreferenceService.UpdatePrefferencesAsync(userId, userYearOfStudy,
                preferences.Select(p => new StudentOptionalPreferenceDto
                {
                    OptionalId = p.OptionalId,
                    StudentId = userId,
                    SortOrder = p.SortOrder,
                    StudyYear = DateTime.Now.Month >= 10 ? DateTime.Now.Year + 2 : DateTime.Now.Year + 1
                })
            );

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePreferences()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            await studentOptionalPreferenceService.RemovePrefferencesAsync(userId);

            return Ok();
        }
    }
}
