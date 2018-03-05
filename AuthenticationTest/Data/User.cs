using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationTest.Data
{
    public class User : IdentityUser
    {
        public User()
        {
        }

        public User(string userName)
            : base(userName)
        {
        }

        public string Rev { get; set; }

        private IList<string> _roles;

        public IList<string> Roles
        {
            get
            {
                _roles = _roles ?? new List<string>();
                return _roles;
            }
            set { _roles = value; }
        }
    }
}
