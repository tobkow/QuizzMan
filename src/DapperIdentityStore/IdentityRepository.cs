using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public partial class IdentityRepository : DapperRepository, IIdentityRepository<User,Role>
    {
        public IdentityRepository(DapperConnectionProvider provider) : base(provider) { }

        public async Task<User> GetUserByNameAsync(string normalizedName)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("@NormalizedUserName", normalizedName, DbType.String);

                var result = await c.QueryAsync<User>(
                    sql: "Users_GetByNormalizedName",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            });
        }

        public async Task<bool> Create(User user)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("Id", user.Id, DbType.Int32);
                p.Add("UserName", user.UserName, DbType.String);
                p.Add("NormalizedUserName", user.NormalizedUserName, DbType.String);
                p.Add("Email", user.Email, DbType.String);
                p.Add("NormalizedEmail", user.NormalizedEmail, DbType.String);
                p.Add("AccessFailedCount", user.AccessFailedCount, DbType.Int32);
                p.Add("LockoutEnabled", user.LockoutEnabled, DbType.Boolean);
                p.Add("LockoutEnd", user.LockoutEnd, DbType.DateTimeOffset);
                p.Add("PasswordHash", user.PasswordHash, DbType.String);
                p.Add("PhoneNumber", user.PhoneNumber, DbType.String);
                p.Add("PhoneNumberConfirmed", user.PhoneNumberConfirmed, DbType.Boolean);
                p.Add("SecurityStamp", user.SecurityStamp, DbType.String);
                p.Add("TwoFactorEnabled", user.TwoFactorEnabled, DbType.Boolean);
                p.Add("ConcurrencyStamp", user.ConcurrencyStamp, DbType.String);

                var result = await c.ExecuteAsync(
                    sql: "Users_Create",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);

            });
        }

        public async Task<bool> Update(User user)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("UserId", user.Id, DbType.Int32);
                p.Add("UserName", user.UserName, DbType.String);
                p.Add("NormalizedUserName", user.NormalizedUserName, DbType.String);
                p.Add("Email", user.Email, DbType.String);
                p.Add("NormalizedEmail", user.NormalizedEmail, DbType.String);
                p.Add("AccessFailedCount", user.AccessFailedCount, DbType.Int32);
                p.Add("LockoutEnabled", user.LockoutEnabled, DbType.Boolean);
                p.Add("LockoutEnd", user.LockoutEnd, DbType.DateTimeOffset);
                p.Add("PasswordHash", user.PasswordHash, DbType.String);
                p.Add("PhoneNumber", user.PhoneNumber, DbType.String);
                p.Add("PhoneNumberConfirmed", user.PhoneNumberConfirmed, DbType.Boolean);
                p.Add("SecurityStamp", user.SecurityStamp, DbType.String);
                p.Add("TwoFactorEnabled", user.TwoFactorEnabled, DbType.Boolean);
                p.Add("ConcurrencyStamp", user.ConcurrencyStamp, DbType.String);

                var result = await c.ExecuteAsync(
                    sql: "Users_Update",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);

            });
        }

        public async Task<bool> Delete(int userId)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();
                p.Add("UserId", userId, DbType.Int32);

                var result = await c.ExecuteAsync(
                    sql: "Users_Delete",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);
            });
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();
                p.Add("UserId", userId, DbType.Int32);

                var result = await c.QueryAsync<User>(
                    sql: "Users_GetById",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();

            });
        }

        public async Task<User> GetByEmail(string normalizedEmail)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();
                p.Add("NormalizedEmail", normalizedEmail, DbType.String);

                var result = await c.QueryAsync<User>(
                    sql: "Users_GetByNormalizedEmail",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();

        });
        }

        public async Task<bool> AddUserToRole(int userId, int roleId)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("UserId", userId, DbType.Int32);
                p.Add("RoleId", roleId, DbType.Int32);

                var result = await c.ExecuteAsync(
                    sql: "UserRoles_AddUserToRole",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);

            });
        }
    }
}
