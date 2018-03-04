using Microsoft.AspNetCore.Identity;

namespace AuthenticationTest.Data
{
    public class Role : IdentityRole
    {
        public string Rev { get; set; }
    }
}
