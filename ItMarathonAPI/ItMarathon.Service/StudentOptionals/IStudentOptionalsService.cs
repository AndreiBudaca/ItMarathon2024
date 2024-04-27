namespace ItMarathon.Service.StudentOptionals
{
    public interface IStudentOptionalsService
    {
        public Task DistributeStudents(int studyYear);

        public Task RemoveOptionals(int studyYear);
    }
}
