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
    public class ReportRepository : IReportRepository
    {
        private readonly string _connectionString;

        public Guid UserID { get; set; }

        public ReportRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CreatedPersons>> GetCreatedPersons(DateTime startDate, DateTime endDate)
        {
            IEnumerable<CreatedPersons> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryAsync<CreatedPersons>("sp_report_createdpersons", param: new { start_Date = startDate, end_Date = endDate, userId = UserID }, commandType: CommandType.StoredProcedure);
            }

            return result.ToList();
        }

        public async Task<IEnumerable<CommunityPlans>> GetCommunityPlans(DateTime startDate, DateTime endDate)
        {
            IEnumerable<CommunityPlans> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryAsync<CommunityPlans>("sp_report_communityplans", param: new { startDate = startDate, endDate = endDate }, commandType: CommandType.StoredProcedure);
            }

            return result.ToList();
        }

        public async Task<IEnumerable<PersonState>> GetPersonState(Guid? areaId, Guid? regionId, Guid? communityId)
        {
            IEnumerable<PersonState> result;

            using(var connection =new SqlConnection(_connectionString))
            {
                result = await connection.QueryAsync<PersonState>("dbo.sp_report_personstate", param: new { areaId = areaId, regionId = regionId, communityId = communityId }, commandType: CommandType.StoredProcedure);
            }

            return result.ToList();
        }

        public async Task<IEnumerable<Person>> GetPersonByBirthday(DateTime? birthDay)
        {
            IEnumerable<Person> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryAsync<Person>(@"SELECT * FROM dbo.Person WHERE (MONTH(BDATE) = MONTH(@BirthDay) AND DAY(BDATE) = DAY(@BirthDay)) AND IS_DELETED = 0 ORDER BY LNAME, FNAME, MNAME", param: new { BirthDay = birthDay }, commandType: CommandType.Text);
            }

            return result;
        }
    }
}
