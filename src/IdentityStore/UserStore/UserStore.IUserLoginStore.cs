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
    public partial class UserStore<TUser> : IUserLoginStore<TUser> where TUser : class,IUser
    {
        public async Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken)
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

            await _userRepo.Create(userlogin);
        }

        public async Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            IUserLogin userLogin = await _userRepo.FindLoginByProviderAndKey(loginProvider, providerKey);

            if (userLogin != null && userLogin.UserId > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                TUser user = await _userRepo.GetUserByIdAsync(userLogin.UserId);
                
                return user ?? default(TUser);
            }

            return default(TUser);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userId = user.Id;

            IList<UserLoginInfo> logins = new List<UserLoginInfo>();
            IList<IUserLogin> userLogins = await _userRepo.GetLoginsByUser(user.Id);

            foreach (UserLogin ul in userLogins)
            {
                logins.Add(new UserLoginInfo(ul.LoginProvider, ul.ProviderKey, ul.ProviderDisplayName));
            }

            return logins;
        }

        public async Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userId = user.Id;

            bool result = await _userRepo.DeleteLogin(user.Id, loginProvider, providerKey);
        }
    }
}
