using Dapper;
using Microsoft.AspNetCore.Identity;
using Phoenix.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Phoenix.Identity.Stores
{
    public class DapperUsersTable
    {
        private readonly SqlConnection _connection;

        public DapperUsersTable(SqlConnection connection)
        {
            _connection = connection;
        }

        #region <--    Users    -->

        public IEnumerable<ApplicationUser> GetUsersAsync()
        {
            string sql = "SELECT * FROM dbo.NetCoreUsers";

            return _connection.Query<ApplicationUser>(sql);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            string sql = "INSERT INTO dbo.NetCoreUsers " +
                "VALUES (@id, @UserName, @NormalizedUserName, @Email, @NormalizedEmail, @EmailConfirmed, " +
                "@PasswordHash, @SecurityStamp, @ConcurrencyStamp, @PhoneNumber, @PhoneNumberConfirmed, " +
                "@TwoFactorEnabled, @LockoutEnd, @LockoutEnabled, @AccessFailedCount)";

            int rows = await _connection.ExecuteAsync(sql, new 
            { 
                user.Id, user.UserName, user.NormalizedUserName,
                user.Email, user.NormalizedEmail, user.EmailConfirmed, 
                user.PasswordHash, user.SecurityStamp, user.ConcurrencyStamp,
                user.PhoneNumber, user.PhoneNumberConfirmed, user.TwoFactorEnabled,
                user.LockoutEnd, user.LockoutEnabled, user.AccessFailedCount
            });

            if (rows > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." });
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            string sql = "DELETE FROM dbo.NetCoreUsers WHERE Id = @id";

            int rows = await _connection.ExecuteAsync(sql, new { user.Id });

            if (rows > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed(new IdentityError { Description = $"Could not delete user {user.Email}." });
        }

        public async Task<ApplicationUser> FindByIdAsync(Guid userId)
        {
            string sql = "SELECT * FROM dbo.NetCoreUsers WHERE Id = @id";

            return await _connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new { Id = userId });
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            string sql = "SELECT * FROM dbo.NetCoreUsers WHERE UserName = @UserName";

            return await _connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new { UserName = userName });
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            string sql = "SELECT * FROM dbo.NetCoreUsers WHERE Email = @Email";

            return await _connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new { Email = email });
        }

        public async Task<IEnumerable<string>> GetRolesAsync(ApplicationUser user)
        {
            string sql = "select r.Name as RoleName from NetCoreUserRoles ur join NetCoreRoles r on r.Id = ur.RoleId where ur.UserId = @userId";

            return await _connection.QueryAsync<string>(sql, new { userId = user.Id.ToString() });
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            string sql = "select count(*) from NetCoreUserRoles ur join NetCoreRoles r on r.Id = ur.RoleId where ur.UserId = @userId and r.Name = @roleName";

            return await _connection.QuerySingleOrDefaultAsync<bool>(sql, new { userId = user.Id.ToString(), roleName = roleName });
        }

        #endregion

        #region <--    User Claims    -->

        public async Task<IList<Claim>> GetUserClaims(ApplicationUser user)
        {
            string sql = "select * from NetCoreClaims where UserId = @userId";

            var claims = await _connection.QueryAsync<ApplicationClaim>(sql, new { userId = user.Id });

            return claims.Select(s => new Claim(s.ClaimType, s.ClaimValue)).ToList();
        }

        public async Task<IdentityResult> AddClaims(ApplicationUser user, IEnumerable<Claim> claims)
        {
            string sql = "insert into NetCoreClaims values ( @userId, @claimType, @claimValue )";

            foreach(var claim in claims)
            {
                int rows = await _connection.ExecuteAsync(sql, new { userId = user.Id.ToString(), claimType = claim.Type, claimValue = claim.Value });
                if (rows < 1)
                    return IdentityResult.Failed(new IdentityError { Description = $"Could not add claim {claim.Type}." });
            }

            return IdentityResult.Success;
        }

        #endregion

        #region <--    Lockout    -->

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser user)
        {
            string sql = "select LockoutEnd from NetCoreUsers where Id = @userId";

            return await _connection.QuerySingleOrDefaultAsync<DateTimeOffset?>(sql, new { userId = user.Id.ToString() });
        }

        public async Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
        {
            string sql = "select LockoutEnabled from NetCoreUsers where Id = @userId";

            return await _connection.QuerySingleOrDefaultAsync<bool>(sql, new { userId = user.Id.ToString() });
        }

        public async Task<int> GetAccessFailedCountAsync(ApplicationUser user)
        {
            string sql = "select AccessFailedCount from NetCoreUsers where Id = @userId";

            return await _connection.QuerySingleOrDefaultAsync<int>(sql, new { userId = user.Id.ToString() });
        }

        public async Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
        {
            string sql = "update NetCoreUsers set AccessFailedCount=AccessFailedCount+1 where Id = @userId;select AccessFailedCount from NetCoreUsers where Id = @userId";

            return await _connection.QuerySingleOrDefaultAsync<int>(sql, new { userId = user.Id.ToString() });
        }

        public async Task<int> ResetAccessFailedCountAsync(ApplicationUser user)
        {
            string sql = "update NetCoreUsers set AccessFailedCount = 0 where Id = @userId";

            return await _connection.ExecuteAsync(sql, new { userId = user.Id.ToString() });
        }

        public async Task<int> SetLockoutEnabledAsync(ApplicationUser user, bool isEnabled)
        {
            string sql = "update NetCoreUsers set LockoutEnabled = @enabled where Id = @userId";

            return await _connection.ExecuteAsync(sql, new { userId = user.Id.ToString(), enabled = isEnabled });
        }

        public async Task<int> SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? offset)
        {
            string sql = "update NetCoreUsers set LockoutEnd = @offset where Id = @userId";

            return await _connection.ExecuteAsync(sql, new { userId = user.Id.ToString(), offset = offset });
        }

        #endregion
    }
}
