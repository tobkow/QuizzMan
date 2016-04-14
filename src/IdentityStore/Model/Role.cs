using System;

namespace QuizzMan.IdentityStore
{
    public class Role : IRole
    {
        public Role() { }

        public Role(string roleName) : this()
        {
            Name = roleName;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public Guid ConcurrencyStamp { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
