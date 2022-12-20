using Nagaira.DataLayer.Core.Repositories;

namespace Nagaira.DataLayer.Core.Standard
{
    public interface IUnitOfWorkBuilder<T>
    {
        IUnitOfWork Build(T selector);
    }
}