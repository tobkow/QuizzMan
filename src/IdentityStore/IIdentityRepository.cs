﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        Task<IList<UserClaim>> GetClaimsForUser(int userId);
        Task<bool> Create(IUserClaim userClaim);

        #endregion

        #region Logins

        Task<bool> Create(IUserLogin user);
        Task<IUserLogin> FindLoginByProviderAndKey(string loginProvider, string providerKey);
        Task<IList<UserLogin>> GetLoginsByUser(int userId);
        Task<bool> DeleteLogin(int userId, string loginProvider, string providerKey);

        #endregion

        #region Roles

        Task<TRole> GetRoleByName(string roleName);
        Task<TRole> GetRoleById(int roleId);
        Task<bool> AddUserToRole(int userId, int roleId);
        Task<IList<string>> GetRolesForUser(int userId);
        Task<IList<TUser>> GetUsersInRole(string roleName);
        Task<bool> RemoveUserFromRole(int roleId, int userId);
        Task<bool> Create(IRole role);
        Task<bool> Update(IRole role);
        Task<bool> DeleteRole(int roleId);

        #endregion

        #region Role claims

        Task<bool> AddRoleClaim(int roleId, string claimType, string claimValue);
        Task<IList<Claim>> GetRoleClaimsByRoleId(int roleId);
        Task<IList<RoleClaim>> GetRoleClaims(int roleId, string claimType, string claimValue);
        Task<bool> DeleteRoleClaim(int roleClaimId);

        #endregion

        #region Users

        Task<TUser> GetUserByNameAsync(string normalizedName);
        Task<TUser> GetUserByIdAsync(int id);
        Task<TUser> GetByEmail(string normalizedEmail);
        Task<bool> Create(TUser user);
        Task<bool> Update(TUser user);
        Task<bool> Delete(int user);

        #endregion
    }
}
