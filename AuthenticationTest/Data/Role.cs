using Microsoft.AspNetCore.Identity;

namespace AuthenticationTest.Data
{
    public class Role : IdentityRole
    {
        public Role()
        {
        }

        public Role(string roleName)
            : base(roleName)
        {
        }

        public string Rev { get; set; }
    }
}
