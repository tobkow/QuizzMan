using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace QuizzMan.IdentityStore
{
    public static class IdentityStoreBuilderExtensions
    {
        public static IdentityBuilder AddDapperIdentityStore<TIdentityRepo>(this IdentityBuilder builder)
            where TIdentityRepo : IIdentityRepository<User,Role>
        {
            builder.Services.TryAdd(GetDefaultServices(builder.UserType, builder.RoleType, typeof(TIdentityRepo)));
            return builder;
        }

        public static IdentityBuilder AddIdentityStore<TIdentityRepo>(this IdentityBuilder builder)
            where TIdentityRepo : IIdentityRepository<User, Role>
        {
            builder.Services.TryAdd(GetDefaultServices(builder.UserType, builder.RoleType, typeof(TIdentityRepo)));
            return builder;
        }

        private static IServiceCollection GetDefaultServices(Type userType, Type roleType, Type idRepoType)
        {
            var userStoreType = typeof(UserStore<,,>).MakeGenericType(userType, idRepoType);
            var roleStoreType = typeof(RoleStore<>).MakeGenericType(roleType, idRepoType);


            var services = new ServiceCollection();
            services.AddScoped(
                typeof(IUserStore<>).MakeGenericType(userType),
                userStoreType);
            services.AddScoped(
                typeof(IRoleStore<>).MakeGenericType(roleType),
                roleStoreType);
            return services;
        }
    }
}
