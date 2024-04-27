using ItMarathon.Data.Entities;

namespace ItMarathon.Service.StudentOptionals
{
    public interface IStudentOptionalsService
    {
        public Task<IEnumerable<StudentOptionalDto>> GetOptionals(int studyYear, int userId);

        public Task DistributeStudents(int studyYear);

        public Task RemoveOptionals(int studyYear);
    }
}
