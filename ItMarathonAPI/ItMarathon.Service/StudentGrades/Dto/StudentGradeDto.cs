namespace ItMarathon.Service.StudentGrades.Dto
{
    public class StudentGradeDto
    {
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public decimal Grade { get; set; }

        public int StudyYear { get; set; }
    }
}
