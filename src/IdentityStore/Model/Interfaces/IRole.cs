using System;

namespace QuizzMan.IdentityStore
{
    public interface IRole
    {
        int Id { get; set; }
        string Name { get; set; }
        string NormalizedName { get; set; }
        Guid ConcurrencyStamp { get; set; }
    }
}
