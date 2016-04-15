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
        public static IServiceCollection AddDapperRepository(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.TryAdd(new ServiceCollection()
                .AddScoped<IIdentityRepository<User, Role>, IdentityRepository>(sp => new IdentityRepository(connectionString)));

            return serviceCollection;
        }
    }
}
