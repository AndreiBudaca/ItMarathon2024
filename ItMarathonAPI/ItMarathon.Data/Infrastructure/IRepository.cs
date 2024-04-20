using System.Linq.Expressions;

namespace ItMarathon.Data.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includeProperties);

    }
}
