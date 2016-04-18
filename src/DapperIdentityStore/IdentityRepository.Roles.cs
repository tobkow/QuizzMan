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
        public async Task<bool> Create(IRole role)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("Name", role.Name, DbType.String);
                p.Add("NormalizedName", role.NormalizedName, DbType.String);
                p.Add("ConcurrencyStamp", role.ConcurrencyStamp, DbType.Guid);

                var result = await c.ExecuteAsync(
                    sql: "Roles_Create",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);

            });
        }

        public async Task<bool> Update(IRole role)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("RoleId", role.Id, DbType.Int32);
                p.Add("Name", role.Name, DbType.String);
                p.Add("NormalizedName", role.NormalizedName, DbType.String);
                p.Add("ConcurrencyStamp", role.ConcurrencyStamp, DbType.Guid);

                var result = await c.ExecuteAsync(
                    sql: "Roles_Update",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);

            });
        }

        public async Task<bool> DeleteRole(int roleId)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("RoleId", roleId, DbType.Int32);

                var result = await c.ExecuteAsync(
                    sql: "Roles_Delete",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);
            });
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();
                p.Add("RoleId", roleId, DbType.Int32);

                var result = await c.QueryAsync<Role>(
                    sql: "Roles_GetById",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();

            });
        }

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
