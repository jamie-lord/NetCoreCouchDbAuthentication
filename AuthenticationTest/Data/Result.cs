using Microsoft.AspNetCore.Identity;

namespace AuthenticationTest.Data
{
    public class Result : IdentityResult
    {
        public Result(bool succeeded)
        {
            Succeeded = succeeded;
        }
    }
}