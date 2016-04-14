namespace QuizzMan.IdentityStore
{
    public interface IUserClaim
    {
        int Id { get; set; }
        int UserId { get; set; }
        string ClaimType { get; set; }
        string ClaimValue { get; set; }
    }
}
