using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Dapper
{
    public partial class UserRepository : RepositoryBase, IUserRepository<User>
    {
        public UserRepository(string connectionString): base (connectionString) { }

        public async Task<User> GetUserByNameAsync(string normalizedName)
        {
            return await WithConnection(async c => {

                var p = new DynamicParameters();
                //p.Add("Id", Id, DbType.Guid);
                var people = await c.QueryAsync<User>(
                    sql: "sp_Person_GetById",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return people.FirstOrDefault(u => u.NormalizedUserName == normalizedName);

            });
        }

        public Task<bool> Create(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await WithConnection(async c => {

                var p = new DynamicParameters();
                p.Add("Id", id, DbType.Guid);
                var people = await c.QueryAsync<User>(
                    sql: "sp_Person_GetById",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return people.FirstOrDefault(u => u.Id == id);

            });
        }

        public Task<User> GetByEmail(string normalizedEmail)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAccessFailedCount(int userId, int accessFailedCount)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}
