using Microsoft.AspNetCore.Identity;
using MyCouch;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationTest.Data
{
    public class UserStore : CouchDbStoreBase, IUserStore<User>, IUserRoleStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>
    {
        public UserStore() : base("users")
        {
            InsertDesignDocument("AuthenticationTest.Data.DesignDocuments.Users.json");
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            if (!user.Roles.Contains(roleName))
            {
                user.Roles.Add(roleName);
                await UpdateAsync(user, cancellationToken);
            }
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var result = await Store.StoreAsync(user);
            if (result != null)
            {
                return new Result(true);
            }
            return new Result(false);
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            var result = await Store.DeleteAsync(user);
            return new Result(result);
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var query = new Query("users", "find_by_email")
            {
                Key = normalizedEmail
            };
            var result = await Store.QueryAsync<User>(query);
            if (result == null || !result.Any())
            {
                return null;
            }
            return result.FirstOrDefault().Value;
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await Store.GetByIdAsync<User>(userId);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var query = new Query("users", "find_by_user_name")
            {
                Key = normalizedUserName
            };
            var result = await Store.QueryAsync<User>(query);
            if (result == null || !result.Any())
            {
                return null;
            }
            return result.FirstOrDefault().Value;
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Roles);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var query = new Query("users", "find_with_role")
            {
                Key = roleName
            };
            var result = await Store.QueryAsync<User>(query);
            if (result == null || !result.Any())
            {
                return null;
            }
            var results = new List<User>();
            foreach (var item in result)
            {
                if (item.Value != null && !results.Contains(item.Value))
                {
                    results.Add(item.Value);
                }
            }
            return results;
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Roles.Contains(roleName));
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            if (user.Roles.Remove(roleName))
            {
                await UpdateAsync(user, cancellationToken);
            }
        }

        public async Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            await Store.StoreAsync(user);
        }

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            await Store.StoreAsync(user);
        }

        public async Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            await Store.StoreAsync(user);
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            await Store.StoreAsync(user);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            await Store.StoreAsync(user);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            await Store.StoreAsync(user);
            return new Result(true);
        }
    }
}
