using Microsoft.AspNetCore.Identity;
using MyCouch;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationTest.Data
{
    public class RoleStore : CouchDbStoreBase, IRoleStore<IdentityRole>
    {
        public RoleStore() : base("roles")
        {
            InsertDesignDocument("AuthenticationTest.Data.DesignDocuments.Roles.json");
        }

        public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            var result = await Store.StoreAsync(role);
            if (result != null)
            {
                return new Result(true);
            }
            return new Result(false);
        }

        public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            var result = await Store.DeleteAsync(role);
            return new Result(result);
        }

        public async Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await Store.GetByIdAsync<IdentityRole>(roleId);
        }

        public async Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var query = new Query("roles", "find_by_name");
            var result = await Store.QueryAsync<IdentityRole>(query);
            if (result == null)
            {
                return null;
            }
            return result.First().Value;
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public async Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            await Store.StoreAsync(role);
        }

        public async Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            await Store.StoreAsync(role);
        }

        public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            await Store.StoreAsync(role);
            return new Result(true);
        }
    }
}
