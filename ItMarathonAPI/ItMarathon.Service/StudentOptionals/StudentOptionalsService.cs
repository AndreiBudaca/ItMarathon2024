using ItMarathon.Data.Entities;
using ItMarathon.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ItMarathon.Service.StudentOptionals
{
    public class StudentOptionalsService : IStudentOptionalsService
    {
        private readonly IRepository<StudentOptional> studentOptionalsRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Course> courseRepository;
        private readonly IUnitOfWork unitOfWork;

        public StudentOptionalsService(IRepository<StudentOptional> studentOptionalsRepository, IRepository<User> userRepository,
            IRepository<Course> courseRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.studentOptionalsRepository = studentOptionalsRepository;
            this.userRepository = userRepository;
            this.courseRepository = courseRepository;
        }

        public async Task DistributeStudents(int studyYear)
        {
            var students = await userRepository.Query(u => u.StudentOptionalPreferences.Where(p => p.YearOfStudy == studyYear))
                .Include(u => u.Grades)
                .ThenInclude(g => g.Course)
                .Where(u => u.Role == Core.Types.UserRole.Student)
                .OrderBy(u => u.Id)
                .AsSplitQuery()
                .ToListAsync();

            var studentsPerYearOfStudy = students.Select(s => new
            {
                StudentId = s.Id,
                StudentYearOfStudy = s.YearOfStudy,
                CreditsCount = s.Grades.Where(g => g.Grade >= 5).Sum(g => g.Course.Credits),
                LastSemesterCredits = s.Grades.Where(g => g.Grade >= 5 && g.StudyYear == (studyYear - 1)).Sum(g => g.Course.Credits),
                LastSemesterGradeSum = s.Grades.Where(g => g.Grade >= 5 && g.StudyYear == (studyYear - 1)).Sum(g => g.Grade * g.Course.Credits),
                Preferences = s.StudentOptionalPreferences
            }).OrderBy(s => s.StudentYearOfStudy)
            .ThenBy(s => s.Preferences.Count == 0 ? 1 : 0)
            .ThenBy(s => s.CreditsCount)
            .ThenBy(s => s.LastSemesterCredits == 0 ? -1 : s.LastSemesterGradeSum / s.LastSemesterCredits)
            .GroupBy(s => s.StudentYearOfStudy);

            var studentsCountPerYear = studentsPerYearOfStudy
                .ToDictionary(g => g.Key, g => g.Count());

            var optionals = await courseRepository.Query()
                .Where(c => c.IsOptional)
                .ToListAsync();

            var studentOptionals = new List<StudentOptional>();

            foreach (var studentYearOfStudyGroup in studentsPerYearOfStudy)
            {
                var yearOfStudy = studentYearOfStudyGroup.Key;
                var nextYearOptionalsPackageGroup = optionals.Where(op => op.YearOfStudy == yearOfStudy + 1)
                    .GroupBy(op => op.OptionalPackage ?? 0);

                foreach (var optionalGroup in nextYearOptionalsPackageGroup)
                {
                    var freeSlots = optionalGroup.Select(g => studentsCountPerYear[yearOfStudy] / optionalGroup.Count()).ToList();
                    for (int i = 0; i < freeSlots.Count && studentsCountPerYear[yearOfStudy] != freeSlots.Sum(); i++)
                    {
                        freeSlots[i] += 1;
                    }

                    foreach (var student in studentYearOfStudyGroup) 
                    {
                        bool selectedFromPreferences = false;
                        foreach (var preference in student.Preferences)
                        {
                            var optional = optionals.Find(c => c.Id == preference.OptionalId);
                            if (optional == null) continue;

                            var optionalIndex = optionalGroup.ToList().IndexOf(optional);

                            if (optionalIndex == -1) continue;
                            if (freeSlots[optionalIndex] == 0) continue;

                            studentOptionals.Add(new StudentOptional
                            {
                                StudentId = student.StudentId,
                                OptionalId = optional.Id,
                                StudyYear = studyYear
                            });

                            freeSlots[optionalIndex] -= 1;
                            selectedFromPreferences = true;

                            break;
                        }

                        if (!selectedFromPreferences)
                        {
                            var optionalIndex = freeSlots.IndexOf(freeSlots.OrderByDescending(slot => slot).First());
                            var optional = optionalGroup.ElementAt(optionalIndex);

                            freeSlots[optionalIndex] -= 1;

                            studentOptionals.Add(new StudentOptional
                            {
                                StudentId = student.StudentId,
                                OptionalId = optional.Id,
                                StudyYear = studyYear
                            });
                        }
                    }
                }
            }

            foreach (var studentOptional in studentOptionals)
            {
                studentOptionalsRepository.Add(studentOptional);
            }

            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<StudentOptionalDto>> GetOptionals(int studyYear, int userId)
        {
            return await studentOptionalsRepository.Query()
                .Where(op => op.StudyYear == studyYear && op.StudentId == userId)
                .Select(op => new StudentOptionalDto
                {
                    Course = new Courses.Dto.CourseDto
                    {
                        Id = op.Optional.Id,
                        Name = op.Optional.Name,
                        Description = op.Optional.Description
                    },
                    Student = new Authentication.Dto.UserDto
                    {
                        Id = op.Student.Id,
                        FirstName = op.Student.FirstName,
                        LastName = op.Student.LastName,
                    }
                })
                .ToListAsync();
        }

        public async Task RemoveOptionals(int studyYear)
        {
            var optionals = await studentOptionalsRepository.Query()
            .Where(so => so.StudyYear == studyYear)
            .ToListAsync();

            foreach (var option in optionals)
            {
                studentOptionalsRepository.Delete(option);
            }

            await unitOfWork.CommitAsync();
        }
    }
}
