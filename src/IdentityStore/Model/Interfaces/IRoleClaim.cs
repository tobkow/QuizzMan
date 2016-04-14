namespace QuizzMan.IdentityStore
{
    public class IRoleClaim
    {
        int Id { get; set; }
        int RoleId { get; set; }
        string ClaimType { get; set; }
        string ClaimValue { get; set; }
    }
}
