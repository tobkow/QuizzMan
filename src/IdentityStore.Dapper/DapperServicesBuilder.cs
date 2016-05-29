using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public class DapperServicesBuilder
    {
        private readonly IServiceCollection _serviceCollection;

        public DapperServicesBuilder(IServiceCollection serviceCollection)
        {
            if (ReferenceEquals(serviceCollection, null))
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            _serviceCollection = serviceCollection;
        }

        public DapperServicesBuilder AddSqlProvider(Action<DapperOptions> optionsAction = null)
        {
            _serviceCollection.AddSingleton(_ => DapperOptionsFactory(optionsAction));
            _serviceCollection.AddScoped<DapperConnectionProvider, DapperSqlConnectionProvider>();

            return this;
        }

        public DapperServicesBuilder AddRepository<TRepo>(Action<DapperOptionsBuilder> optionsAction = null)
            where TRepo : DapperRepository
        {
            //_serviceCollection.TryAdd(new ServiceCollection()
            //    .AddScoped<IIdentityRepository<User, Role>, TIdentityRepo>(sp => new RepositoryBase(connectionString)));
            _serviceCollection.AddScoped<TRepo>();
            //_serviceCollection.AddSingleton(_ => DbContextOptionsFactory<TContext>(optionsAction));
            //_serviceCollection.AddSingleton<DbContextOptions>(p => p.GetRequiredService<DbContextOptions<TContext>>());

            //_serviceCollection.AddScoped(typeof(TContext), DbContextActivator.CreateInstance<TContext>);

            return this;
        }

        private static DapperOptions DapperOptionsFactory(Action<DapperOptions> optionsAction)
        {
            var options = new DapperOptions();

            optionsAction?.Invoke(options);

            return options;
        }
    }
}
