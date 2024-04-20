namespace ItMarathon.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
