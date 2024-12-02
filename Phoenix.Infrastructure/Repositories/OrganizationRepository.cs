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
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly string _connectionString;

        public Guid UserID { get; set; }

        public OrganizationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            IEnumerable<Organization> organizations;

            using (var connection = new SqlConnection(_connectionString))
            {
                organizations = await connection.QueryAsync<Organization>(@"select o.ID, CAST(o.LEVEL as nvarchar(256)) as LEVEL, o.NAME + ' ' + LOWER(ot.NAME) as ORG_NAME from Organization o join OrganizationType ot on ot.ID = o.ORG_TYPE_ID order by o.LEVEL");
            }

            return organizations;
        }

        public async Task<int> CountPersonInOrganization(Guid orgId)
        {
            int personCount = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                personCount = await connection.ExecuteScalarAsync<int>(@"SELECT COUNT(PSN_ID) FROM PositionInOrganization pio WHERE pio.DISMISS_DATE IS NOT NULL AND pio.ORG_ID = @org_id", param: new { @org_id = orgId });
            }
            return personCount;
        }

        public async Task<IEnumerable<Position>> GetPositions()
        {
            IEnumerable<Position> positions;

            using(var connection=new SqlConnection(_connectionString))
            {
                positions = await connection.QueryAsync<Position>(@"select * from dbo.Position order by 2");
            }

            return positions;
        }

        public async Task<IEnumerable<PositionInOrganization>> GetPersonPositions(Guid psnId)
        {
            IEnumerable<PositionInOrganization> positions;
            
            using (var connection = new SqlConnection(_connectionString))
            {
                positions = await connection.QueryAsync<PositionInOrganization>(@"SELECT pio.ID, pio.ORG_ID, o.NAME + ' ' + LOWER(ot.NAME) AS ORG_NAME, pio.PSN_ID, pio.POS_ID, p.POS_NAME, pio.APPOINT_DATE, pio.DISMISS_DATE FROM dbo.PositionInOrganization pio JOIN dbo.Organization o ON o.ID = pio.ORG_ID JOIN dbo.OrganizationType ot ON ot.ID = o.ORG_TYPE_ID JOIN dbo.Position p ON p.ID = pio.POS_ID  WHERE PSN_ID = @psnId ORDER BY APPOINT_DATE DESC", new { psnId = psnId });
            }

            return positions;
        }

        public async Task<IEnumerable<Guid>> CreateOrUpdatePersonPositions(Guid psnId, List<PositionInOrganization> positions)
        {
            IEnumerable<Guid> positionsID;

            using (var connection = new SqlConnection(_connectionString))
            {
                foreach (var pos in positions)
                {
                    switch (pos.ACTION)
                    {
                        case 0:     // создать должность в организации
                            await connection.ExecuteAsync(@"INSERT INTO dbo.PositionInOrganization VALUES ( NEWID(), @orgID, @psnID, @posID, @apDate, @dmDate )", new { @orgID = pos.ORG_ID, @psnID = psnId, @posID = pos.POS_ID, @apDate = pos.APPOINT_DATE, @dmDate = pos.DISMISS_DATE });
                            break;
                        case 1:     // обновить должность в организации
                            await connection.ExecuteAsync(@"UPDATE dbo.PositionInOrganization SET ORG_ID = @orgID, POS_ID = @posID, APPOINT_DATE = @apDate, DISMISS_DATE = @dmDate WHERE ID = @id", new { @orgID = pos.ORG_ID, @posID = pos.POS_ID, @apDate = pos.APPOINT_DATE, @dmDate = pos.DISMISS_DATE, @id = pos.ID });
                            break;
                        case 2:     // удалить должность
                            await connection.ExecuteAsync(@"DELETE FROM dbo.PositionInOrganization WHERE ID = @id", new { @id = pos.ID });
                            break;
                    }
                    //// создать должность в организации
                    //if (pos.ID == Guid.Empty)
                    //{
                    //    await connection.ExecuteAsync(@"INSERT INTO dbo.PositionInOrganization VALUES ( NEWID(), @orgID, @psnID, @posID, @apDate, @dmDate )", new { @orgID = pos.ORG_ID, @psnID=psnId, @posID = pos.POS_ID, @apDate = pos.APPOINT_DATE, @dmDate = pos.DISMISS_DATE });
                    //}
                    //// обновить долность в организации
                    //else
                    //{
                    //    await connection.ExecuteAsync(@"UPDATE dbo.PositionInOrganization SET ORG_ID = @orgID, POS_ID = @posID, APPOINT_DATE = @apDate, DISMISS_DATE = @dmDate WHERE ID = @id", new { @orgID = pos.ORG_ID, @posID = pos.POS_ID, @apDate = pos.APPOINT_DATE, @dmDate = pos.DISMISS_DATE, @id = pos.ID });
                    //}
                }

                positionsID = await connection.QueryAsync<Guid>(@"SELECT ID FROM dbo.PositionInOrganization WHERE PSN_ID = @psnID", new { @psnID = psnId });
            }

            return positionsID;
        }
    }
}
