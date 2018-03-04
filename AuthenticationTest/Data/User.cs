using Microsoft.AspNetCore.Identity;

namespace AuthenticationTest.Data
{
    public class User : IdentityUser
    {
        public string Rev { get; set; }
    }
}
