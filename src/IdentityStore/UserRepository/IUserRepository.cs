using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore
{
    public interface IUserRepository<TUser> where TUser : IUser
    {
        Task<TUser> FindByNameAsync(string normalizedName);
        Task<TUser> FindByIdAsync(int id);
        Task<bool> Create(TUser user);
        Task<bool> Update(TUser user);
        Task<bool> Delete(int user);
    }
}
