using System;

namespace QuizzMan.IdentityStore
{
    public interface IRole
    {
        int RoleId { get; set; }
        Guid RoleGuid { get; set; }
        string Name { get; set; }
    }
}
