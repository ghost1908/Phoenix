using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure
{
    public class LogChangeService
    {
        private readonly string _connectionString;
        
        public LogChangeService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task WriteLog(SqlConnection connection, string tableName, Guid rowId, string columnName, object valueOld, object valueNew, Guid userId, DateTime dateStamp)
        {

            await connection.ExecuteAsync(@"INSERT INTO dbo.LogChange VALUES ( @userId, @dStamp, @tName, @rowId, @cName, @vOld, @vNew )",
                new
                {
                    @userId = userId,
                    @dStamp = dateStamp,
                    @tName = tableName,
                    @rowId = rowId,
                    @cName = columnName,
                    @vOld = valueOld,
                    @vNew = valueNew
                });
        }
    }
}
