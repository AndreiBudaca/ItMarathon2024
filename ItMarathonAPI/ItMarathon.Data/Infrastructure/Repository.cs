using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ItMarathon.Data.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ItMarathonContext itMarathonContext;
        private readonly DbSet<TEntity> dbSet;

        public Repository(ItMarathonContext dataContext)
        {
            itMarathonContext = dataContext;
            dbSet = dataContext.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            itMarathonContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
    }
}
