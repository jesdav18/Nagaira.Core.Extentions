using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nagaira.DataLayer.Core.Repositories;

namespace Nagaira.DataLayer.Core.Standard
{
    public class GenericUnitOfWorkBuilder<T> : IUnitOfWorkBuilder<T>, IUnitOfWorkConfigurationBuilder<T>
    {
        protected IServiceProvider services;
        protected Dictionary<T, Func<IServiceProvider, DbContext>> _dbContextActivator = new Dictionary<T, Func<IServiceProvider, DbContext>>();
        protected Dictionary<T, IUnitOfWork> _unitOfWorks = new Dictionary<T, IUnitOfWork>();

        public GenericUnitOfWorkBuilder(IServiceProvider services)
        {
            this.services = services;
        }
        public virtual IUnitOfWork Build(T selector)
        {
            if (_unitOfWorks.ContainsKey(selector)) return _unitOfWorks[selector];

            DbContext contexto = _dbContextActivator[selector].Invoke(services);
            UnitOfWork unitOfWork = new UnitOfWork(contexto);
            _unitOfWorks.Add(selector, unitOfWork);
            return unitOfWork;
        }

        public virtual void AddResolver<TDbContext>(T selector) where TDbContext : DbContext
        {
            _dbContextActivator.Add(selector, (services) =>
            {
                TDbContext dbContext = services.GetService<TDbContext>()!;
                return dbContext!;
            });
        }
    }

    public interface IUnitOfWorkConfigurationBuilder<T>
    {
        void AddResolver<TDbContext>(T selector) where TDbContext : DbContext;
    }

}