using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace QuizzMan.IdentityStore
{
    public partial class UserStore<TUser,TRole,TIdentityRepo> : UserStoreBase<TUser,TRole,TIdentityRepo>,
        IUserStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserClaimStore<TUser>,
        IUserEmailStore<TUser>,
        IUserLockoutStore<TUser>,
        IUserLoginStore<TUser>,
        IUserPhoneNumberStore<TUser>,
        IUserRoleStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IUserTwoFactorStore<TUser>
        where TIdentityRepo : IIdentityRepository<TUser,TRole>
        where TUser : class,IUser
        where TRole : class, IRole
    {
        private UserStore() : base(){ }
        private ILogger log;
        private bool debugLog = false;
        

        public UserStore(ILogger<UserStore<TUser,TRole,TIdentityRepo>> logger,TIdentityRepo userRepository) : base(userRepository)
        {
            log = logger;

            if (userRepository == null) { throw new ArgumentNullException(nameof(userRepository)); }

            //debugLog = config.GetOrDefault("AppSettings:UserStoreDebugEnabled", false);

            if (debugLog) { log.LogInformation("constructor"); }
        }

        Task<string> IUserPasswordStore<TUser>.GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PasswordHash);
        }

        Task<bool> IUserPasswordStore<TUser>.HasPasswordAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.PasswordHash != null);
        }

        Task IUserPasswordStore<TUser>.SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        async Task IUserClaimStore<TUser>.AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
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
                bool result = await _identityRepo.Create(userClaim);
            }
        }

        async Task<IList<Claim>> IUserClaimStore<TUser>.GetClaimsAsync(TUser user, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            cancellationToken.ThrowIfCancellationRequested();

            IList<Claim> claims = new List<Claim>();
            IList<UserClaim> userClaims = await _identityRepo.GetClaimsForUser(user.Id);

            foreach (UserClaim uc in userClaims)
            {
                Claim c = new Claim(uc.ClaimType, uc.ClaimValue);
                claims.Add(c);
            }

            return claims;
        }

        async Task<IList<TUser>> IUserClaimStore<TUser>.GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            return await _identityRepo.GetUsersByClaim(claim.Type, claim.Value);
        }

        async Task IUserClaimStore<TUser>.RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
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
                await _identityRepo.DeleteClaimForUser(user.Id, claim.Type, claim.Value);
            }
        }

        async Task IUserClaimStore<TUser>.ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
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

            await _identityRepo.DeleteClaimForUser(user.Id, claim.Type, claim.Value);

            UserClaim userClaim = new UserClaim();
            userClaim.UserId = user.Id;
            userClaim.ClaimType = newClaim.Type;
            userClaim.ClaimValue = newClaim.Value;

            cancellationToken.ThrowIfCancellationRequested();

            bool result = await _identityRepo.Create(userClaim);
        }

        async Task<TUser> IUserEmailStore<TUser>.FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return await _identityRepo.GetByEmail(normalizedEmail);
        }

        Task<string> IUserEmailStore<TUser>.GetEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Email);
        }

        Task<bool> IUserEmailStore<TUser>.GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.EmailConfirmed);
        }

        Task<string> IUserEmailStore<TUser>.GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.NormalizedEmail);
        }

        Task IUserEmailStore<TUser>.SetEmailAsync(TUser user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.Email = email;

            return Task.FromResult(0);
        }

        Task IUserEmailStore<TUser>.SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.EmailConfirmed = confirmed;

            return Task.FromResult(0);
        }

        Task IUserEmailStore<TUser>.SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.NormalizedEmail = normalizedEmail;

            return Task.FromResult(0);
        }

        Task<int> IUserLockoutStore<TUser>.GetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.AccessFailedCount);
        }

        Task<bool> IUserLockoutStore<TUser>.GetLockoutEnabledAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.LockoutEnabled);
        }

        Task<DateTimeOffset?> IUserLockoutStore<TUser>.GetLockoutEndDateAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.LockoutEnd);
        }

        Task<int> IUserLockoutStore<TUser>.IncrementAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.AccessFailedCount++;

            return Task.FromResult(user.AccessFailedCount);
        }

        Task IUserLockoutStore<TUser>.ResetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.AccessFailedCount = 0;

            return Task.FromResult(0);
        }

        Task IUserLockoutStore<TUser>.SetLockoutEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.LockoutEnabled = enabled;

            return Task.FromResult(0);
        }

        Task IUserLockoutStore<TUser>.SetLockoutEndDateAsync(TUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.LockoutEnd = lockoutEnd;

            return Task.FromResult(0);
        }

        async Task IUserLoginStore<TUser>.AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            var userlogin = new UserLogin();
            userlogin.UserId = user.Id;
            userlogin.LoginProvider = login.LoginProvider;
            userlogin.ProviderKey = login.ProviderKey;
            userlogin.ProviderDisplayName = login.ProviderDisplayName;

            cancellationToken.ThrowIfCancellationRequested();

            await _identityRepo.Create(userlogin);
        }

        async Task<TUser> IUserLoginStore<TUser>.FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            IUserLogin userLogin = await _identityRepo.FindLoginByProviderAndKey(loginProvider, providerKey);

            if (userLogin != null && userLogin.UserId > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                TUser user = await _identityRepo.GetUserByIdAsync(userLogin.UserId);

                return user ?? default(TUser);
            }

            return default(TUser);
        }

        async Task<IList<UserLoginInfo>> IUserLoginStore<TUser>.GetLoginsAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userId = user.Id;

            IList<UserLoginInfo> logins = new List<UserLoginInfo>();
            IList<UserLogin> userLogins = await _identityRepo.GetLoginsByUser(user.Id);

            foreach (UserLogin ul in userLogins)
            {
                logins.Add(new UserLoginInfo(ul.LoginProvider, ul.ProviderKey, ul.ProviderDisplayName));
            }

            return logins;
        }

        async Task IUserLoginStore<TUser>.RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userId = user.Id;

            bool result = await _identityRepo.DeleteLogin(user.Id, loginProvider, providerKey);
        }

        Task<string> IUserPhoneNumberStore<TUser>.GetPhoneNumberAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PhoneNumber);
        }

        Task<bool> IUserPhoneNumberStore<TUser>.GetPhoneNumberConfirmedAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        Task IUserPhoneNumberStore<TUser>.SetPhoneNumberAsync(TUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        Task IUserPhoneNumberStore<TUser>.SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        async Task IUserRoleStore<TUser>.AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
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

        async Task<IList<string>> IUserRoleStore<TUser>.GetRolesAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await _identityRepo.GetRolesForUser(user.Id);
        }

        async Task<IList<TUser>> IUserRoleStore<TUser>.GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            return await _identityRepo.GetUsersInRole(roleName);
        }

        async Task<bool> IUserRoleStore<TUser>.IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
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

        async Task IUserRoleStore<TUser>.RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
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

        Task<string> IUserSecurityStampStore<TUser>.GetSecurityStampAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.SecurityStamp);
        }

        Task IUserSecurityStampStore<TUser>.SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        Task<bool> IUserTwoFactorStore<TUser>.GetTwoFactorEnabledAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.TwoFactorEnabled);
        }

        Task IUserTwoFactorStore<TUser>.SetTwoFactorEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
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

        public IdentityErrorDescriber ErrorDescriber { get; set; }
        public bool AutoSaveChanges { get; set; } = true;
    }
}
