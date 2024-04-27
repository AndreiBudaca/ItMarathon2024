using ItMarathon.Data.Entities;
using ItMarathon.Data.Infrastructure;
using ItMarathon.Service.Courses.Dto;
using Microsoft.EntityFrameworkCore;

namespace ItMarathon.Service.Courses
{
    public class CourseService : ICoursesService
    {
        private readonly IRepository<Course> coursesRepository;
        private readonly IUnitOfWork unitOfWork;

        public CourseService(IRepository<Course> coursesRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.coursesRepository = coursesRepository;
        }

        public async Task<CourseDto?> GetAsync(int id)
        {
            var dbCourse = await coursesRepository.Query().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (dbCourse == null )  return null;

            return new CourseDto
            {
                Id = id,
                Name = dbCourse.Name,
                Description = dbCourse.Description,
                YearOfStudy = dbCourse.YearOfStudy,
                Semester = dbCourse.Semester,
                IsOptional = dbCourse.IsOptional,
                Credits = dbCourse.Credits,
                OptionalPackage = dbCourse.OptionalPackage,
            };
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            return await coursesRepository.Query()
                .Select(course => new CourseDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Description = course.Description,
                    YearOfStudy = course.YearOfStudy,
                    Semester = course.Semester,
                    IsOptional = course.IsOptional,
                    Credits = course.Credits,
                    OptionalPackage = course.OptionalPackage,
                }).ToListAsync();
        }

        public async Task AddAsync(CourseDto course)
        {
            coursesRepository.Add(new Course
            {
                Name = course.Name,
                Description = course.Description,
                YearOfStudy = course.YearOfStudy,
                Semester = course.Semester,
                IsOptional = course.IsOptional,
                Credits = course.Credits,
                OptionalPackage = course.OptionalPackage,
            });

            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dbCourse = await coursesRepository.Query().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (dbCourse != null)
            {
                coursesRepository.Delete(dbCourse);
            }

            await unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(CourseDto course)
        {
            var dbCourse = await coursesRepository.Query().Where(c => c.Id == course.Id).FirstOrDefaultAsync();

            if (dbCourse == null) return;

            dbCourse.Name = course.Name;
            dbCourse.Description = course.Description;
            dbCourse.YearOfStudy = course.YearOfStudy;
            dbCourse.Semester = course.Semester;
            dbCourse.IsOptional = course.IsOptional;
            dbCourse.Credits = course.Credits;
            dbCourse.OptionalPackage = course.OptionalPackage;

            await unitOfWork.CommitAsync();
        }
    }
}
