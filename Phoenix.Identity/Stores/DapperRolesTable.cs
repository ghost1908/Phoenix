using Dapper;
using Microsoft.AspNetCore.Identity;
using Phoenix.Identity.Entities;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Phoenix.Identity.Stores
{
    public class DapperRolesTable
    {
        private readonly SqlConnection _connection;

        public DapperRolesTable(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role)
        {
            string sql = "INSERT INTO dbo.NetCoreRoles " +
                "VALUES (@id, @name, @normalizedName)";

            int rows = await _connection.ExecuteAsync(sql, new { role.Id, role.Name, role.NormalizedName });

            if (rows > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed(new IdentityError { Description = $"Could not insert role {role.Name}." });
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role)
        {
            string sql = "DELETE FROM dbo.NetCoreRoles WHERE Id = @id";

            int rows = await _connection.ExecuteAsync(sql, new { role.Id });

            if (rows > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed(new IdentityError { Description = $"Could not delete role {role.Name}." });
        }

        public async Task<ApplicationRole> FindByIdAsync(Guid roleId)
        {
            string sql = "SELECT * FROM dbo.NetCoreRoles WHERE Id = @id";

            return await _connection.QuerySingleOrDefaultAsync<ApplicationRole>(sql, new { Id = roleId });
        }

        public async Task<ApplicationRole> FindByNameAsync(string roleName)
        {
            string sql = "SELECT * FROM dbo.NetCoreRoles WHERE Name = @RoleName";

            return await _connection.QuerySingleOrDefaultAsync<ApplicationRole>(sql, new { RoleName = roleName });
        }
    }
}
