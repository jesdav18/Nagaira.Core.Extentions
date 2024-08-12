using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Transactions;

namespace Nagaira.DataLayer.Core.Repositories
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;

        public IUnitOfWork UnitOfWork { get; private set; }

        public EntityRepository(DbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            UnitOfWork = unitOfWork;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(ICollection<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public List<TEntity> Where(Expression<Func<TEntity, bool>> query)
        {
            return _dbContext.Set<TEntity>().Where(query).ToList();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> query)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(query)!;
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveAll(Expression<Func<TEntity, bool>> query)
        {
            List<TEntity> entidades = Where(query);
            _dbContext.Set<TEntity>().RemoveRange(entidades);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(ICollection<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> query, int? take = null)
        {
            if (take.HasValue) return await _dbContext.Set<TEntity>().Where(query).Take(take.Value).ToListAsync();
            return await _dbContext.Set<TEntity>().Where(query).ToListAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> query)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(query)!;
        }

        public async Task RemoveAllAsync(Expression<Func<TEntity, bool>> query)
        {
            List<TEntity> entidades = await WhereAsync(query);
            _dbContext.Set<TEntity>().RemoveRange(entidades);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        private List<TEntity> WhereReadUncommitted(Expression<Func<TEntity, bool>> query, int? take = null)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions()
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted
                }))
            {

                List<TEntity> result = new List<TEntity>();
                if (take.HasValue) result = _dbContext.Set<TEntity>().Where(query).Take(take.Value).ToList();
                else result = _dbContext.Set<TEntity>().Where(query).ToList();

                scope.Complete();
                return result;
            }
        }

        private async Task<TEntity> FirstOrDefaultReadUncommittedAsync(Expression<Func<TEntity, bool>> query)
        {
            using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            }))
            {
                TEntity result = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(query);
                scope.Complete();
                return result;
            }
        }

        public async Task<List<TEntity>> WhereAsyncReadUncommited(Expression<Func<TEntity, bool>> query, int? take = null)
        {
            return await Task.Run(() => WhereReadUncommitted(query, take));
        }

        public async Task<TEntity> FirstOrDefaultAsyncReadUncommited(Expression<Func<TEntity, bool>> query)
        {
            return await FirstOrDefaultReadUncommittedAsync(query);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> query)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(query);
        }
    }
}