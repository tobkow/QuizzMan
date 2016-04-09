namespace QuizzMan.IdentityStore
{
    public interface IUserClaim
    {
        int UserId { get; set; }
        string ClaimType { get; set; }
        string ClaimValue { get; set; }
    }
}
