using ItMarathon.Data.Entities;
using ItMarathon.Data.Infrastructure;
using ItMarathon.Service.StudentGrades.Dto;

namespace ItMarathon.Service.StudentGrades
{
    public class StudentGradeService : IStudentGradesService
    {
        private readonly IRepository<StudentGrade> gradeRepository;
        private readonly IUnitOfWork unitOfWork;

        public StudentGradeService(IRepository<StudentGrade> gradeRepository, IUnitOfWork unitOfWork)
        {
            this.gradeRepository = gradeRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task AddStudentGradeAsync(StudentGradeDto studentGrade)
        {
            gradeRepository.Add(new StudentGrade
            {
                CourseId = studentGrade.CourseId,
                StudentId = studentGrade.StudentId,
                Grade = studentGrade.Grade,
                StudyYear = studentGrade.StudyYear
            });

            await unitOfWork.CommitAsync();
        }

        public async Task DeleteStudentGradeAsync(StudentGradeDto studentGradeDto)
        {
            var dbGrade = gradeRepository.Query()
                .FirstOrDefault(g => g.CourseId == studentGradeDto.CourseId && g.StudentId == studentGradeDto.StudentId);

            if (dbGrade == null) return;

            gradeRepository.Delete(dbGrade);

            await unitOfWork.CommitAsync();
        }

        public async Task UpdateStudentGradeAsync(StudentGradeDto studentGradeDto)
        {
            var dbGrade = gradeRepository.Query()
                .FirstOrDefault(g => g.CourseId == studentGradeDto.CourseId && g.StudentId == studentGradeDto.StudentId);

            if (dbGrade == null) return;

            dbGrade.Grade = studentGradeDto.Grade;
            dbGrade.StudyYear = studentGradeDto.StudyYear;

            await unitOfWork.CommitAsync();
        }
    }
}
