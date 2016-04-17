using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public partial class IdentityRepository
    {
        public async Task<Role> GetRoleByName(string roleName)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();
                p.Add("Name", roleName, DbType.String);

                var result = await c.QueryAsync<Role>(
                    sql: "Roles_GetByName",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();

            });
        }

        public async Task<IList<string>> GetRolesForUser(int userId)
        {
            return await DapperProvider.WithConnection(async c =>
            {

                var p = new DynamicParameters();

                p.Add("Name", userId, DbType.Int32);

                var result = await c.QueryAsync<Role>(
                    sql: "Roles_GetRolesForUser",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return result.Select(role => role.Name).ToList();
            });
        }

        public async Task<IList<User>> GetUsersInRole(string roleName)
        {
            return await DapperProvider.WithConnection(async c =>
            {

                var p = new DynamicParameters();

                p.Add("Name", roleName, DbType.String);

                var result = await c.QueryAsync<User>(
                    sql: "Users_GetUsersInRole",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return result.ToList();
            });
        }

        public async Task<bool> RemoveUserFromRole(int roleId, int userId)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();
                p.Add("UserId", userId, DbType.Int32);
                p.Add("RoleId", roleId, DbType.Int32);

                var result = await c.ExecuteAsync(
                    sql: "UserRoles_DeleteByUserIdAndRoleId",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);
            });
        }
    }
}
