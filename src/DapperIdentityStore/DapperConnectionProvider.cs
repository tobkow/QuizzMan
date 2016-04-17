using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public abstract class DapperConnectionProvider : IDapperConnectionProvider
    {
        public abstract Task<T> WithConnection<T>(Func<DbConnection, Task<T>> getData);
    }
}
