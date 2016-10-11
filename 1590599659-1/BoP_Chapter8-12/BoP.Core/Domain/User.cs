using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{
    /// <summary>
    /// The User class, as the name implies is the "User"
    /// role in the system. This role is placed on any stakeholder 
    /// in the domain to provide the required information
    /// for logging on and off of the system.  This Role is 
    /// created after a user of the web site registers.
    /// </summary>
    /// 
    [Serializable]
    public class User:Role
    {
        private const string ROLE_NAME = "User";

        public User() : base(ROLE_NAME) { }

        public string UserId { get; set; }
        public string Password { get; set; }
        public char Active { get; set; }
        
    }
}
