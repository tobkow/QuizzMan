using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace QuizzMan.IdentityStore.UserStore
{
    public partial class UserStore<TUser,TRole,TIdentityRepo> : UserStoreBase<TUser,TRole,TIdentityRepo>,
        IUserClaimStore<TUser>
        where TIdentityRepo : IIdentityRepository<TUser,TRole>
        where TUser : class, IUser
        where TRole : class, IRole
    {
        public async Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            foreach (var claim in claims)
            {
                var userClaim = new UserClaim();
                userClaim.UserId = user.Id;
                userClaim.ClaimType = claim.Type;
                userClaim.ClaimValue = claim.Value;
                cancellationToken.ThrowIfCancellationRequested();
                bool result = await _userRepo.Create(userClaim);
            }
        }

        public async Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            cancellationToken.ThrowIfCancellationRequested();

            IList<Claim> claims = new List<Claim>();
            IList<UserClaim> userClaims = await _userRepo.GetClaimsForUser(user.Id);

            foreach (UserClaim uc in userClaims)
            {
                Claim c = new Claim(uc.ClaimType, uc.ClaimValue);
                claims.Add(c);
            }

            return claims;
        }

        public async Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            return await _userRepo.GetUsersByClaim(claim.Type, claim.Value);
        }

        public async Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            foreach (var claim in claims)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _userRepo.DeleteClaimForUser(user.Id, claim.Type, claim.Value);
            }
        }

        public async Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            if (newClaim == null)
            {
                throw new ArgumentNullException(nameof(newClaim));
            }

            await _userRepo.DeleteClaimForUser(user.Id, claim.Type, claim.Value);

            UserClaim userClaim = new UserClaim();
            userClaim.UserId = user.Id;
            userClaim.ClaimType = newClaim.Type;
            userClaim.ClaimValue = newClaim.Value;

            cancellationToken.ThrowIfCancellationRequested();

            bool result = await _userRepo.Create(userClaim);
        }
    }
}
