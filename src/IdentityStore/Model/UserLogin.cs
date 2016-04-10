namespace QuizzMan.IdentityStore
{
    public class UserLogin : IUserLogin
    {
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderKey { get; set; }
    }
}
