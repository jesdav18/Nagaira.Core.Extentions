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
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(ICollection<TEntity> entities);
        Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> query);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> query);
        Task RemoveAllAsync(Expression<Func<TEntity, bool>> query);
    }
}
