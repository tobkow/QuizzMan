using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public interface IDapperConnectionProvider
    {
        Task<T> WithConnection<T>(Func<DbConnection, Task<T>> getData);
    }
}