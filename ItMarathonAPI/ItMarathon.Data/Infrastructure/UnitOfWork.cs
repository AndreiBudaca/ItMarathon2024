
namespace ItMarathon.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ItMarathonContext itMarathonContext;

        public UnitOfWork(ItMarathonContext itMarathonContext)
        {
            this.itMarathonContext = itMarathonContext;
        }

        public async Task CommitAsync()
        {
            await itMarathonContext.SaveChangesAsync();
        }
    }
}
