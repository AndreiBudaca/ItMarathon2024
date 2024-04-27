using ItMarathon.Data.Entities;
using ItMarathon.Data.Infrastructure;
using ItMarathon.Service.Courses.Dto;
using ItMarathon.Service.StudentOptionalPreferences.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ItMarathon.Service.StudentOptionalPreferences
{
    public class StudentOptionalPreferenceService : IStudentOptionalPreferenceService
    {
        private readonly IRepository<StudentOptionalPreference> preferencesRepository;
        private readonly IRepository<StudentOptional> optionalRepository;
        private readonly IRepository<Course> courseRepository;
        private readonly IUnitOfWork unitOfWork;

        public StudentOptionalPreferenceService(IRepository<StudentOptionalPreference> preferencesRepository,
            IRepository<Course> courseRepository, IRepository<StudentOptional> optionalRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.preferencesRepository = preferencesRepository;
            this.courseRepository = courseRepository;
            this.optionalRepository = optionalRepository;
        }

        public async Task AddPrefferencesAsync(int userId, int userYearOfStudy, IEnumerable<StudentOptionalPreferenceDto> preferences)
        {
            var existingPreferences = await preferencesRepository.Query().Where(p => p.StudentId == userId).AnyAsync();
            if (existingPreferences) throw new Exception("Preferences were already added!");

            var preferencesCourses = preferences.Select(p => p.OptionalId);

            var coursesAreInvalid = await courseRepository.Query()
                .Where(c => preferencesCourses.Contains(c.Id))
                .AnyAsync(c => !c.IsOptional);

            if (coursesAreInvalid) throw new Exception("All courses must be optional and for the next year of study!");

            await Add(userId, preferences);

            await unitOfWork.CommitAsync();
        }

        public async Task RemovePrefferencesAsync(int userId)
        {

            await Remove(userId);
            await unitOfWork.CommitAsync();
        }

        public async Task UpdatePrefferencesAsync(int userId, int userYearOfStudy, IEnumerable<StudentOptionalPreferenceDto> preferences)
        {
            var preferencesCourses = preferences.Select(p => p.OptionalId);

            var coursesAreInvalid = await courseRepository.Query()
                .Where(c => preferencesCourses.Contains(c.Id))
                .AnyAsync(c => !c.IsOptional || c.YearOfStudy != (userYearOfStudy + 1));

            await Remove(userId);
            await Add(userId, preferences.OrderByDescending(p => p.SortOrder));

            await unitOfWork.CommitAsync();
        }


        private async Task Remove(int userId)
        {
            var existingPreferences = await preferencesRepository.Query().Where(p => p.StudentId == userId)
                .AsNoTracking().ToListAsync();

            foreach (var p in existingPreferences)
            {
                preferencesRepository.Delete(p);
            }
        }

        private async Task Add(int userId, IEnumerable<StudentOptionalPreferenceDto> preferences)
        {
            var studyYears = preferences.Select(p => p.StudyYear).Distinct();

            var optionalsExist = await optionalRepository.Query()
                .Where(o => studyYears.Contains(o.StudyYear))
                .AnyAsync();

            var courses = courseRepository.Query()
                .Where(c => preferences.Select(p => p.OptionalId).Contains(c.Id));

            var packageSortOrder = courses.Select(c => c.OptionalPackage).Distinct().Select(c => new OptionalPackageSortOrderDto { OptionalPackage = c, Count = 0 }).ToList();

            if (optionalsExist) throw new Exception("The optionals have been chosen already");

            foreach (var preference in preferences)
            {
                var sortOrder = packageSortOrder.First(so => courses.First(c => preference.OptionalId == c.Id).OptionalPackage == so.OptionalPackage);
                preferencesRepository.Add(new StudentOptionalPreference
                {
                    StudentId = userId,
                    OptionalId = preference.OptionalId,
                    SortOrder = sortOrder.Count,
                    YearOfStudy = preference.StudyYear,
                });

                sortOrder.Count += 1;
            }
        }

        public async Task<IEnumerable<StudentOptionalPreferenceDto>> GetAsync(int userId)
        {
            return await preferencesRepository.Query()
                .Where(p => p.StudentId == userId)
                .Select(p => new StudentOptionalPreferenceDto
                {
                    StudentId = p.StudentId,
                    OptionalId = p.OptionalId,
                    SortOrder = p.SortOrder
                }).ToListAsync();
        }
    }
}
