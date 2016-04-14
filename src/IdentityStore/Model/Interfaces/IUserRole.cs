using System;

namespace QuizzMan.IdentityStore
{
    public interface IUserRole
    {
        int UserId { get; set; }
        int RoleId { get; set; }
    }
}
