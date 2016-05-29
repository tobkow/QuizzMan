using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public partial class IdentityRepository
    {
        public async Task<bool> AddRoleClaim(int roleId, string claimType, string claimValue)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("RoleId", roleId, DbType.Int32);
                p.Add("ClaimType", claimType, DbType.String);
                p.Add("ClaimValue", claimValue, DbType.String);

                var result = await c.ExecuteAsync(
                    sql: "RoleClaims_Create",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);

            });
        }

        public async Task<IList<Claim>> GetRoleClaimsByRoleId(int roleId)
        {
            return await DapperProvider.WithConnection(async c =>
            {

                var p = new DynamicParameters();

                p.Add("RoleId", roleId, DbType.Int32);

                var result = await c.QueryAsync<RoleClaim>(
                    sql: "RoleClaims_GetByRole",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return result.Select(rc => new Claim(rc.ClaimType,rc.ClaimValue)).ToList();
            });
        }

        public async Task<IList<RoleClaim>> GetRoleClaims(int roleId, string claimType, string claimValue)
        {
            return await DapperProvider.WithConnection(async c =>
            {

                var p = new DynamicParameters();

                p.Add("RoleId", roleId, DbType.Int32);
                p.Add("ClaimType", claimType, DbType.String);
                p.Add("ClaimValue", claimValue, DbType.String);

                var result = await c.QueryAsync<RoleClaim>(
                    sql: "RoleClaims_Get",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return result.ToList();
            });
        }

        public async Task<bool> DeleteRoleClaim(int roleClaimId)
        {
            return await DapperProvider.WithConnection(async c => {

                var p = new DynamicParameters();

                p.Add("Id", roleClaimId, DbType.Int32);

                var result = await c.ExecuteAsync(
                    sql: "RoleClaims_Delete",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(result);

            });
        }
    }
}
