using ItMarathon.Service.Courses.Dto;

namespace ItMarathon.Service.Courses
{
    public interface ICoursesService
    {
        public Task<IEnumerable<CourseDto>> GetAllAsync(int? yearOfStudy, bool onlyOptionals = false);
        
        public Task<CourseDto?> GetAsync(int id);

        public Task AddAsync(CourseDto course);

        public Task UpdateAsync(CourseDto course);

        public Task DeleteAsync(int id);
    }
}
