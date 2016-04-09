namespace QuizzMan.IdentityStore
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string NormalizedUserName { get; set; }
        public string UserName { get; set; }
    }
}
