using ItMarathon.Data.Entities;

namespace ItMarathon.Service.StudentOptionals
{
    public interface IStudentOptionalsService
    {
        //public Task<IEnumerable<StudentOptionalDto>> Get

        public Task DistributeStudents(int studyYear);

        public Task RemoveOptionals(int studyYear);
    }
}
