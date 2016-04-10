using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public partial class UserRepository
    {
        public Task<bool> Create(IUserLogin user)
        {
            throw new NotImplementedException();
        }

        public Task<IUserLogin> FindLoginByProviderAndKey(string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }

        public Task<IList<IUserLogin>> GetLoginsByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLogin(int userId, string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }
    }
}
