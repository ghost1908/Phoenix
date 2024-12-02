using Dapper;
using Phoenix.Infrastructure.Entities;
using Phoenix.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Repositories
{
    public class HubRepository : IHubRepository
    {
        private readonly string _connectionString;

        public Guid UserID { get; set; }

        public HubRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task ConnectAsync(string connectionId, string userDisplayName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var connExists = await connection.QuerySingleAsync<int>(@"SELECT COUNT(*) FROM hub.Person WHERE CONN_ID = @conn_id", param: new { @conn_id = connectionId });
                if (connExists == 0)
                    await connection.ExecuteAsync(@"INSERT INTO hub.Person VALUES ( @conn_id, null, @user_name )", param: new { @conn_id = connectionId, @user_name = userDisplayName });
            }
        }

        public async Task<IEnumerable<HubPerson>> DisconnectAsync(string connectionId)
        {
            IEnumerable<HubPerson> persons;

            using (var connection = new SqlConnection(_connectionString))
            {
                persons = await connection.QueryAsync<HubPerson>(@"SELECT * FROM hub.Person WHERE CONN_ID = @conn_id", param: new { @conn_id = connectionId });
                await connection.ExecuteAsync(@"DELETE FROM hub.Person WHERE CONN_ID = @conn_id", param: new { @conn_id = connectionId });
            }

            return persons;
        }

        public async Task<IEnumerable<HubPerson>> GetEditingPersonsAsync()
        {
            IEnumerable<HubPerson> persons;

            using (var connection = new SqlConnection(_connectionString))
            {
                persons = await connection.QueryAsync<HubPerson>(@"SELECT * FROM hub.Person");
            }

            return persons;
        }

        //public async Task PersonEditingAsync(string connectionId, string personId)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.ExecuteAsync(@"UPDATE hub.Person SET PSN_ID = @psn_id WHERE CONN_ID = @conn_id", param: new { @conn_id = connectionId, @psn_id = personId });
        //    }
        //}

        public async Task PersonEditingAsync(string connectionId, string personId, string userDisplayName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var connExists = await connection.QuerySingleAsync<int>(@"SELECT COUNT(*) FROM hub.Person WHERE CONN_ID = @conn_id", param: new { @conn_id = connectionId });
                if (connExists == 0)
                    await connection.ExecuteAsync(@"INSERT INTO hub.Person VALUES ( @conn_id, @psn_id, @user_name )", param: new { @conn_id = connectionId, @psn_id = personId, @user_name = userDisplayName });
            }
        }

        public async Task<string> GetDisplayNameAsync(string personId)
        {
            string userDisplayName = string.Empty;

            using (var connection = new SqlConnection(_connectionString))
            {
                userDisplayName = await connection.QuerySingleAsync<string>(@"SELECT TOP 1 USR_DSPL_NAME FROM hub.Person WHERE PSN_ID = @psn_id", param: new { @psn_id = personId });
            }

            if (string.IsNullOrWhiteSpace(userDisplayName)) userDisplayName = "unknown";

            return userDisplayName;
        }
    }
}
