using System;

namespace QuizzMan.IdentityStore
{
    public class Role : IRole
    {
        public int RoleId { get; set; }
        public Guid RoleGuid { get; set; }
        public string Name { get; set; }
    }
}
