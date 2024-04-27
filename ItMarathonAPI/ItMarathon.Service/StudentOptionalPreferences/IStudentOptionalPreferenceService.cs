using ItMarathon.Service.StudentOptionalPreferences.Dto;

namespace ItMarathon.Service.StudentOptionalPreferences
{
    public interface IStudentOptionalPreferenceService
    {
        public Task<IEnumerable<StudentOptionalPreferenceDto>> GetAsync(int userId);

        public Task AddPrefferencesAsync(int userId, int userYearOfStudy, IEnumerable<StudentOptionalPreferenceDto> preferences);

        public Task UpdatePrefferencesAsync(int userId, int userYearOfStudy, IEnumerable<StudentOptionalPreferenceDto> preferences);

        public Task RemovePrefferencesAsync(int userId);
    }
}
