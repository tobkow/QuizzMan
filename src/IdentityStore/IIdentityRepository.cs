using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore
{
    public interface IIdentityRepository<TUser,TRole>
        where TUser : IUser
        where TRole : IRole
    {
        #region Claims

        Task<bool> DeleteClaimForUser(int userId, string claimType, string claimValue);
        Task<IList<TUser>> GetUsersByClaim(string claimType, string claimValue);
        Task<IList<IUserClaim>> GetClaimsForUser(int userId);
        Task<bool> Save(IUserClaim userClaim);

        #endregion

        #region Logins

        Task<bool> Create(IUserLogin user);
        Task<IUserLogin> FindLoginByProviderAndKey(string loginProvider, string providerKey);
        Task<IList<IUserLogin>> GetLoginsByUser(int userId);
        Task<bool> DeleteLogin(int userId, string loginProvider, string providerKey);

        #endregion

        #region Roles

        Task<TRole> GetRoleByName(string roleName);
        Task<bool> AddUserToRole(int userId, int roleId);
        Task<IList<string>> GetRolesForUser(int userId);
        Task<IList<TUser>> GetUsersInRole(string roleName);
        Task<bool> RemoveUserFromRole(int roleId, int userId);

        #endregion

        #region Users

        Task<TUser> GetUserByNameAsync(string normalizedName);
        Task<TUser> GetUserByIdAsync(int id);
        Task<TUser> GetByEmail(string normalizedEmail);
        Task<bool> Create(TUser user);
        Task<bool> Update(TUser user);
        Task<bool> Delete(int user);
        Task<bool> Save(TUser user);

        #endregion
    }
}
