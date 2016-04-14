using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public partial class IdentityRepository
    {
        public Task<IList<IUserClaim>> GetClaimsForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save(IUserClaim userClaim)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteClaimForUser(int userId, string claimType, string claimValue)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetUsersByClaim(string claimType, string claimValue)
        {
            throw new NotImplementedException();
        }
    }
}
