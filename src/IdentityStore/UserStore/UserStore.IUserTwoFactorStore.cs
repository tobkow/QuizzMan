using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace QuizzMan.IdentityStore.UserStore
{
    public partial class UserStore<TUser,TRole,TIdentityRepo> : UserStoreBase<TUser,TRole,TIdentityRepo>,
        IUserTwoFactorStore<TUser>
        where TIdentityRepo : IIdentityRepository<TUser,TRole>
        where TUser : class, IUser
        where TRole : class, IRole
    {
        public Task<bool> GetTwoFactorEnabledAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }
    }
}
