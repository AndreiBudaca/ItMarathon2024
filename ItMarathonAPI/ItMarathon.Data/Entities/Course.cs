namespace ItMarathon.Data.Entities
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int YearOfStudy { get; set; }

        public int Credits { get; set; }

        public int Semester { get; set; }

        public bool IsOptional { get; set; }

        public int? OptionalPackage { get; set; }
    }
}
