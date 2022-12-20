using System.Linq.Expressions;

namespace Nagaira.DataLayer.Core.Repositories
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void RemoveAll(Expression<Func<TEntity, bool>> query);
        void AddRange(ICollection<TEntity> entities);
        IUnitOfWork UnitOfWork { get; }
        IQueryable<TEntity> AsQueryable();
        List<TEntity> Where(Expression<Func<TEntity, bool>> query);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> query);
    }
}
