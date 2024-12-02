using Phoenix.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Phoenix.Infrastructure.Entities;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly string _connectionString;

        public Guid UserID { get; set; }

        public AdminRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<TableData>> GetTableDatas(string tableName)
        {
            IEnumerable<TableData> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryAsync<TableData>("cfg.sp_gettabledatabyobjectname", param: new { table = tableName }, commandType: CommandType.StoredProcedure);
            }

            return result;
        }

        public async Task<IEnumerable<ObjectAccess>> GetObjectAccesses()
        {
            IEnumerable<ObjectAccess> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryAsync<ObjectAccess>("SELECT * FROM cfg.ObjectAccess");
            }

            return result;
        }

        public async Task<IEnumerable<UserAccess>> GetUserAccesses(string userId)
        {
            IEnumerable<UserAccess> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryAsync<UserAccess>("SELECT * FROM cfg.UserAccess WHERE UserId = @userId", param: new { userId = userId });
            }

            return result;
        }

        public async Task<IEnumerable<string>> GetUserAccessNames(string userId)
        {
            IEnumerable<string> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryAsync<string>("cfg.sp_getobjectaccessnames", param: new { UserId = userId }, commandType: CommandType.StoredProcedure);
            }

            return result;
        }

        public async Task<int> AddUserAccess(Guid userId, Guid objectId, object objectValue)
        {
            int result = -1;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.ExecuteAsync("INSERT INTO cfg.UserAccess VALUES ( @uId, @oId, CAST(@oVal AS nvarchar(256)) )", param: new { uId = userId, @oId = objectId, @oVal = objectValue });
            }

            return result;
        }
    }
}
