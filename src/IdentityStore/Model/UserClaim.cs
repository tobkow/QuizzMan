namespace QuizzMan.IdentityStore
{
    public class UserClaim : IUserClaim
    {
        public int UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue {get; set; }
    }
}
