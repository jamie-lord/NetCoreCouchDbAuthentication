using Microsoft.AspNetCore.Identity;
using MyCouch;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationTest.Data
{
    public class RoleStore : CouchDbStoreBase, IRoleStore<Role>
    {
        public RoleStore() : base("roles")
        {
            InsertDesignDocument("AuthenticationTest.Data.DesignDocuments.Roles.json");
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            var result = await Store.StoreAsync(role);
            if (result != null)
            {
                return new Result(true);
            }
            return new Result(false);
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            var result = await Store.DeleteAsync(role);
            return new Result(result);
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await Store.GetByIdAsync<Role>(roleId);
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var query = new Query("roles", "find_by_name")
            {
                Key = normalizedRoleName
            };
            var result = await Store.QueryAsync<Role>(query);
            if (result == null || !result.Any())
            {
                return null;
            }
            return result.First().Value;
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public async Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            await Store.StoreAsync(role);
        }

        public async Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            await Store.StoreAsync(role);
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            await Store.StoreAsync(role);
            return new Result(true);
        }
    }
}
