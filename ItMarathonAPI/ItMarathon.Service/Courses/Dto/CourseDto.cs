namespace ItMarathon.Service.Courses.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int YearOfStudy { get; set; }

        public bool IsOptional { get; set; }

        public int? OptionalPackage { get; set; }
    }
}
