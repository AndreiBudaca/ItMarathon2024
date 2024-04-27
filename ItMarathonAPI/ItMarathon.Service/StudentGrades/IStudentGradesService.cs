using ItMarathon.Service.StudentGrades.Dto;

namespace ItMarathon.Service.StudentGrades
{
    public interface IStudentGradesService
    {
        public Task AddStudentGradeAsync(StudentGradeDto studentGrade);

        public Task UpdateStudentGradeAsync(StudentGradeDto studentGradeDto);

        public Task DeleteStudentGradeAsync(StudentGradeDto studentGradeDto);
    }
}
