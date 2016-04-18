using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace QuizzMan.IdentityStore
{
    public partial class UserStore<TUser,TRole,TIdentityRepo> : UserStoreBase<TUser,TRole,TIdentityRepo>,
        IUserRoleStore<TUser>
        where TIdentityRepo : IIdentityRepository<TUser,TRole>
        where TUser : class, IUser
        where TRole : class, IRole
    {
        public async Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(nameof(roleName));
            }

            var role = await _identityRepo.GetRoleByName(roleName);
            bool result = false;
            if (role != null)
            {
                result = await _identityRepo.AddUserToRole(user.Id, role.Id);
            }
        }

        public async Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await _identityRepo.GetRolesForUser(user.Id);
        }

        public async Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            return await _identityRepo.GetUsersInRole(roleName);
        }

        public async Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(nameof(roleName));
            }

            IList<string> roles = await _identityRepo.GetRolesForUser(user.Id);

            foreach (string r in roles)
            {
                if (string.Equals(r, roleName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(nameof(roleName));
            }

            var role = await _identityRepo.GetRoleByName(roleName);
            
            if (role != null)
            {
                await _identityRepo.RemoveUserFromRole(role.Id, user.Id);
            }
        }
    }
}
