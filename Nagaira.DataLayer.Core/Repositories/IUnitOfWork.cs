using Nagaira.DataLayer.Core.Standard;
using System.Data.Common;

namespace Nagaira.DataLayer.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        IEnumerable<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map, params object[] parameters);
        IQueryable<T> RawSqlQuery<T>(string query, params object[] parameters) where T : class;
        Task<IEnumerable<T>> RawSqlQueryAsync<T>(string query, Func<DbDataReader, T> map, params object[] parameters);
        Task ExecQueryAsync(string query, params object[] parameters);
        void ExecQuery(string query, params object[] parameters);
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        void Commit();
        void RollBack();
        bool SaveChanges();
        Task<bool> SaveChangesAsync();
        void SetCommandTimeout(int timeOut = 30);
    }   
}