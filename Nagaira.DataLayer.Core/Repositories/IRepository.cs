using System.Linq.Expressions;

namespace Nagaira.DataLayer.Core.Repositories
{
    public interface IRepository<TEntity>
    {
        IUnitOfWork UnitOfWork { get; }
        void Add(TEntity entity);
        void AddRange(ICollection<TEntity> entities);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(ICollection<TEntity> entities);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(ICollection<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveAll(Expression<Func<TEntity, bool>> query);
        Task RemoveAllAsync(Expression<Func<TEntity, bool>> query);
        IQueryable<TEntity> AsQueryable();
        List<TEntity> Where(Expression<Func<TEntity, bool>> query);
        Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> query);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> query);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> query);
    }
}
