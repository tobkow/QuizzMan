using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public partial class IdentityRepository
    {
        public async Task<IList<UserClaim>> GetClaimsForUser(int userId)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();
                p.Add("UserId", userId, DbType.Int32);

                var result = await c.QueryAsync<UserClaim>(
                    sql: "UserClaims_GetClaimsForUser",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return result.ToList();

            });
        }

        public async Task<bool> Create(IUserClaim userClaim)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("UserId", userClaim.UserId, DbType.Int32);
                p.Add("ClaimType", userClaim.ClaimType, DbType.String);
                p.Add("ClaimValue", userClaim.ClaimValue, DbType.String);

                var result = await c.ExecuteAsync(
                    sql: "UserClaims_Create",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);

            });
        }

        public async Task<bool> DeleteClaimForUser(int userId, string claimType, string claimValue)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("UserId", userId, DbType.Int32);
                p.Add("ClaimType", claimType, DbType.String);
                p.Add("ClaimValue", claimValue, DbType.String);

                var result = await c.ExecuteAsync(
                    sql: "UserClaims_DeleteClaimForUser",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);
            });
        }

        public async Task<IList<User>> GetUsersByClaim(string claimType, string claimValue)
        {
            return await DapperProvider.WithConnection(async c => {

            var p = new DynamicParameters();

            p.Add("ClaimType", claimType, DbType.String);
            p.Add("ClaimValue", claimValue, DbType.String);

            var result = await c.QueryAsync<User>(
                sql: "Users_GetUsersByClaim",
                param: p,
                commandType: CommandType.StoredProcedure);
            return result.ToList();
        });
        }
    }
}
