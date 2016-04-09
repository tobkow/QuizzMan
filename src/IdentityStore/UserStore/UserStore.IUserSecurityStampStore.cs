using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace QuizzMan.IdentityStore.UserStore
{
    public partial class UserStore<TUser> : UserStoreBase<TUser>, IUserSecurityStampStore<TUser> where TUser : class,IUser
    {
        public Task<string> GetSecurityStampAsync(TUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
