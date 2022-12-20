using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Nagaira.DataLayer.Core.Repositories
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext dbContext;

        public IUnitOfWork UnitOfWork { get; private set; }

        public EntityRepository(DbContext dbContext, IUnitOfWork unitOfWork)
        {
            this.dbContext = dbContext;
            UnitOfWork = unitOfWork;
        }

        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(ICollection<TEntity> entities)
        {
            dbContext.Set<TEntity>().AddRange(entities);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return dbContext.Set<TEntity>().AsQueryable();
        }

        public List<TEntity> Where(Expression<Func<TEntity, bool>> query)
        {
            return dbContext.Set<TEntity>().Where(query).ToList();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> query)
        {
            return dbContext.Set<TEntity>().FirstOrDefault(query);
        }

        public void Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveAll(Expression<Func<TEntity, bool>> query)
        {
            List<TEntity> entidades = Where(query);
            dbContext.Set<TEntity>().RemoveRange(entidades);
        }
    }
}