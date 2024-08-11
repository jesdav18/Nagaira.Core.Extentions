using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Nagaira.DataLayer.Core.Repositories;
using System.Data;
using System.Data.Common;

namespace Nagaira.DataLayer.Core.Standard
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private IDbContextTransaction? _transaccion;
        private bool _disposed = false;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new EntityRepository<TEntity>(_dbContext, this);
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {

            _transaccion = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaccion == null) return;

            _transaccion.Commit();
            _transaccion.Dispose();
            _transaccion = null;
        }

        public virtual void RollBack()
        {
            if (_transaccion == null) return;

            _transaccion.Rollback();
            _transaccion.Dispose();
            _transaccion = null;
        }

        public bool SaveChanges()
        {
            if (_transaccion == null)
            {
                try
                {
                    BeginTransaction();
                    _dbContext.SaveChanges();
                    Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    RollBack();
                    SaveChangesException(ex);
                    return false;
                }
            }

            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                SaveChangesException(ex);
                return false;
            }
        }

        protected virtual void SaveChangesException(Exception ex)
        {
            throw ex;
        }

        public IEnumerable<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map, params object[] parameters)
        {
            using (DbCommand command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                _dbContext.Database.OpenConnection();
                command.Parameters.AddRange(parameters);
                if (_transaccion != null) command.Transaction = _transaccion.GetDbTransaction();
                using (DbDataReader result = command.ExecuteReader())
                {
                    List<T> entities = new List<T>();
                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }
                    return entities;
                }
            }
        }

        public IQueryable<T> RawSqlQuery<T>(string query, params object[] parameters) where T : class
        {
            return _dbContext.Set<T>().FromSqlRaw(query, parameters);
        }

        public async Task<IEnumerable<T>> RawSqlQueryAsync<T>(string query, Func<DbDataReader, T> map, params object[] parameters)
        {
            using (DbCommand command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                await _dbContext.Database.OpenConnectionAsync();
                command.Parameters.AddRange(parameters);
                if (_transaccion != null) command.Transaction = _transaccion.GetDbTransaction();
                using (DbDataReader result = await command.ExecuteReaderAsync())
                {
                    List<T> entities = new List<T>();
                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }
                    return entities;
                }
            }
        }

        public void ExecQuery(string query, params object[] parameters)
        {
            using (DbCommand command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                _dbContext.Database.OpenConnection();
                command.Parameters.AddRange(parameters);
                if (_transaccion != null) command.Transaction = _transaccion.GetDbTransaction();
                command.ExecuteNonQuery();
            }
        }

        public async Task ExecQueryAsync(string query, params object[] parameters)
        {
            using (DbCommand command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                await _dbContext.Database.OpenConnectionAsync();
                if (_transaccion != null) command.Transaction = _transaccion.GetDbTransaction();
                command.Parameters.AddRange(parameters);
                await command.ExecuteNonQueryAsync();
            }
        }
        public void SetCommandTimeout(int timeOut = 30)
        {
            _dbContext.Database.SetCommandTimeout(TimeSpan.FromSeconds(timeOut));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _transaccion?.Dispose();
                _dbContext.Dispose();
            }

            _disposed = true;

        }

        public async Task<bool> SaveChangesAsync()
        {
            if (_transaccion == null)
            {
                try
                {
                    BeginTransaction();
                    await _dbContext.SaveChangesAsync();
                    Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    RollBack();
                    SaveChangesException(ex);
                    return false;
                }
            }

            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                SaveChangesException(ex);
                return false;
            }
        }
    }
}