using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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


        public Task AddRangeAsync(ICollection<TEntity> entities)
        {
            return _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> query)
        {
            return _dbContext.Set<TEntity>().Where(query).ToListAsync();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> query)
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync(query)!;
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
    }
}