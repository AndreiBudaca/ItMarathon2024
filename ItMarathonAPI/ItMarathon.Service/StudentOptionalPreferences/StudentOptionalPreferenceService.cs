using ItMarathon.Data.Entities;
using ItMarathon.Data.Infrastructure;
using ItMarathon.Service.Courses.Dto;
using ItMarathon.Service.StudentOptionalPreferences.Dto;
using Microsoft.EntityFrameworkCore;

namespace ItMarathon.Service.StudentOptionalPreferences
{
    public class StudentOptionalPreferenceService : IStudentOptionalPreferenceService
    {
        private readonly IRepository<StudentOptionalPreference> preferencesRepository;
        private readonly IRepository<Course> courseRepository;
        private readonly IUnitOfWork unitOfWork;

        public StudentOptionalPreferenceService(IRepository<StudentOptionalPreference> preferencesRepository,
            IRepository<Course> courseRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.preferencesRepository = preferencesRepository;
            this.courseRepository = courseRepository;
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

            Add(userId, preferences);

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
            Add(userId, preferences);

            await unitOfWork.CommitAsync();
        }


        private async Task Remove(int userId)
        {
            var existingPreferences = await preferencesRepository.Query().Where(p => p.StudentId == userId).ToListAsync();

            foreach (var p in existingPreferences)
            {
                preferencesRepository.Delete(p);
            }
        }

        private void Add(int userId, IEnumerable<StudentOptionalPreferenceDto> preferences)
        {
            for (var i = 0; i < preferences.Count(); ++i)
            {
                preferencesRepository.Add(new StudentOptionalPreference
                {
                    StudentId = userId,
                    OptionalId = preferences.ElementAt(i).StudentId,
                    SortOrder = i
                });
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
