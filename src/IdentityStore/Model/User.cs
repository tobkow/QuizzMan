using System;

namespace QuizzMan.IdentityStore
{
    public class User : IUser
    {
        public User() { }

        public User(string userName) : this()
        {
            UserName = userName;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string NormalizedUserName { get; set; }
        public int AccessFailedCount { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public object UserGuid { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public override string ToString()
        {
            return UserName;
        }
    }
}
