using System;

namespace QuizzMan.IdentityStore
{
    public interface IUserLogin
    {
        int UserId { get; set; }
        string LoginProvider { get; set; }
        string ProviderDisplayName { get; set; }
        string ProviderKey { get; set; }
    }
}
