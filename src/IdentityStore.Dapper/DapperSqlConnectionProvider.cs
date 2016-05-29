using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace QuizzMan.IdentityStore.Dapper
{
    public class DapperSqlConnectionProvider : DapperConnectionProvider
    {
        private readonly string _connectionString;

        public DapperSqlConnectionProvider(DapperOptions options)
        {
            _connectionString = options.ConnectionString;
        }

        public override async Task<T> WithConnection<T>(Func<DbConnection, Task<T>> getData)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    return await getData(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
    }
}
