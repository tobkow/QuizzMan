namespace QuizzMan.IdentityStore
{
    public interface IUser
    {
        int Id { get; set; }
        string UserName { get; set; }
        string NormalizedUserName { get; set; }
    }
}
