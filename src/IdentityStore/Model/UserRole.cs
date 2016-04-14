using System;

namespace QuizzMan.IdentityStore
{
    public class UserRole : IUserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
