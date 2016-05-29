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
        public async Task<bool> Create(IUserLogin userLogin)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("UserId", userLogin.UserId, DbType.Int32);
                p.Add("LoginProvider", userLogin.LoginProvider, DbType.String);
                p.Add("ProviderDisplayName", userLogin.ProviderDisplayName, DbType.String);
                p.Add("ProviderKey", userLogin.ProviderKey, DbType.String);

                var result = await c.ExecuteAsync(
                    sql: "UserLogins_Create",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);

            });
        }

        public async Task<IUserLogin> FindLoginByProviderAndKey(string loginProvider, string providerKey)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("LoginProvider", loginProvider, DbType.String);
                p.Add("ProviderKey", providerKey, DbType.String);

                var result = await c.QueryAsync<UserLogin>(
                    sql: "UserLogins_GetByProviderAndKey",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();

            });
        }

        public async Task<IList<UserLogin>> GetLoginsByUser(int userId)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("UserId", userId, DbType.Int32);

                var result = await c.QueryAsync<UserLogin>(
                    sql: "UserLogins_GetByUserId",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return result.ToList();

            });
        }

        public async Task<bool> DeleteLogin(int userId, string loginProvider, string providerKey)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("UserId", userId, DbType.Int32);
                p.Add("LoginProvider", loginProvider, DbType.String);
                p.Add("ProviderKey", providerKey, DbType.String);

                var result = await c.ExecuteAsync(
                    sql: "UserLogins_DeleteForUserAndProvider",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);
            });
        }
    }
}
