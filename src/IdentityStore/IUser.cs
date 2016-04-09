using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore
{
    public interface IUser
    {
        int Id { get; set; }
        string UserName { get; set; }
        string NormalizedUserName { get; set; }
    }
}
