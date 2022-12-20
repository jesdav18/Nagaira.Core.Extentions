using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Nagaira.DataLayer.Core.Extentions
{
    public static class DbContextExtentions
    {
        public static IEnumerable<T> RawSqlQuery<T>(this DbContext dbContext, string query, Func<DbDataReader, T> map, params object[] parameters)
        {
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandTimeout = dbContext.Database.GetCommandTimeout() ?? 100;
                command.CommandType = CommandType.Text;
                dbContext.Database.OpenConnection();
                command.Parameters.AddRange(parameters);
                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();
                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }

                    return entities;
                }
            }
        }
    }
}
