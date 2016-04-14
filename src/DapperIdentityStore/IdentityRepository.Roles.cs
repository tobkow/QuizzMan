using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public partial class IdentityRepository
    {
        public Task<Role> GetRoleByName(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserToRole(int roleId, Guid roleGuid, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromRole(int roleId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
