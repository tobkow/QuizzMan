using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public static class ServiceCollectionExtensions
    {
        public static DapperServicesBuilder UseDapper(this IServiceCollection serviceCollection)
        {
            return new DapperServicesBuilder(serviceCollection);
        }
    }
}
