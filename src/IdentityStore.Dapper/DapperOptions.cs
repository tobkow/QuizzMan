using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public class DapperOptions
    {
        public DapperOptions() { }

        public DapperOptions(DapperOptions options)
        {
            _connectionString = options?.ConnectionString;
        }

        private string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
            }
        }
    }
}
