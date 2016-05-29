using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public abstract class DapperRepository
    {
        private DapperConnectionProvider _provider { get; set; }

        public DapperRepository(DapperConnectionProvider provider)
        {
            if (ReferenceEquals(provider, null))
            {
                throw new ArgumentNullException(nameof(provider));
            }

            _provider = provider;
        }

        protected DapperConnectionProvider DapperProvider => _provider;
    }
}
