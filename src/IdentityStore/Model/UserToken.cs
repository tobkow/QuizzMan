using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzMan.IdentityStore.Model
{
    public class UserToken
    {
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
