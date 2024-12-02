using Microsoft.AspNetCore.Identity;
using Phoenix.Identity.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Identity.Stores
{
    public class RoleStore : IRoleStore<ApplicationRole>
    {
        private readonly DapperRolesTable _rolesTables;

        public RoleStore(DapperRolesTable rolesTable)
        {
            _rolesTables = rolesTable;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return await _rolesTables.CreateAsync(role);
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return await _rolesTables.DeleteAsync(role);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (roleId == null)
                throw new ArgumentNullException(nameof(roleId));

            Guid idGuid;

            if (!Guid.TryParse(roleId, out idGuid))
                throw new ArgumentException("Not a valid Guid id", nameof(roleId));

            return await _rolesTables.FindByIdAsync(idGuid);
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (normalizedRoleName == null)
                throw new ArgumentNullException(nameof(normalizedRoleName));

            return await _rolesTables.FindByNameAsync(normalizedRoleName);
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
