using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Claims;

namespace QuizzMan.IdentityStore
{
    public class RoleStore<TUser,TRole,TIdentityRepo> : RoleStoreBase<TUser,TRole,TIdentityRepo>,
        IRoleStore<TRole>,
        IRoleClaimStore<TRole>
        where TIdentityRepo : IIdentityRepository<TUser, TRole>
        where TUser : class, IUser
        where TRole : class, IRole
    {
        public async Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            bool result = await _identityRepo.AddRoleClaim(role.Id, claim.Type, claim.Value);
        }

        public async Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return await _identityRepo.GetRoleClaimsByRoleId(role.Id);
        }

        public async Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            var roleClaims = await _identityRepo.GetRoleClaims(role.Id, claim.Type, claim.Value);
            foreach (var rclaim in roleClaims)
            {
                await _identityRepo.Delete(rclaim.Id);
            }
        }
    }
}
