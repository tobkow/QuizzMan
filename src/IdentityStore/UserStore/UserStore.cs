using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Identity;

namespace QuizzMan.IdentityStore
{
    public partial class UserStore<TUser> : UserStoreBase<TUser> where TUser : class,IUser
    {
        private UserStore() : base(){ }
        private ILogger log;
        private bool debugLog = false;
        

        public UserStore(ILogger<UserStore<TUser>> logger,IUserRepository<TUser> userRepository)
        {
            log = logger;

            if (userRepository == null) { throw new ArgumentNullException(nameof(userRepository)); }

            //debugLog = config.GetOrDefault("AppSettings:UserStoreDebugEnabled", false);

            if (debugLog) { log.LogInformation("constructor"); }
        }
        
        public IdentityErrorDescriber ErrorDescriber { get; set; }
        public bool AutoSaveChanges { get; set; } = true;
    }
}
