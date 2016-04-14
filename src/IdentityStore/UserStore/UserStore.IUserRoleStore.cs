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

            var role = await _userRepo.GetRoleByName(roleName);
            bool result = false;
            if (role != null)
            {
                result = await _userRepo.AddUserToRole(user.Id, role.Id);
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

            return await _userRepo.GetRolesForUser(user.Id);
        }

        public async Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            return await _userRepo.GetUsersInRole(roleName);
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

            IList<string> roles = await _userRepo.GetRolesForUser(user.Id);

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

            var role = await _userRepo.GetRoleByName(roleName);
            
            if (role != null)
            {
                await _userRepo.RemoveUserFromRole(role.Id, user.Id);
            }
        }
    }
}
