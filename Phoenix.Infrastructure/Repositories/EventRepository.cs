using Dapper;
using Phoenix.Infrastructure.Entities;
using Phoenix.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace Phoenix.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly string _connectionString;

        public Guid UserID { get; set; }

        public EventRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            IEnumerable<Project> projects;

            using (var connection = new SqlConnection(_connectionString))
            {
                projects = await connection.QueryAsync<Project>(@"SELECT * FROM dbo.Project ORDER BY PRJT_NAME");
            }

            return projects;
        }

        public async Task<IEnumerable<Project>> GetProjects(int access)
        {
            IEnumerable<Project> projects;

            using (var connection = new SqlConnection(_connectionString))
            {
                projects = await connection.QueryAsync<Project>(@"SELECT prj.* FROM dbo.Project prj JOIN dbo.ProjectEvent prje ON prje.PRJT_ID = prj.ID WHERE prje.EVT_ACCESS = 1 OR prje.EVT_ACCESS & @access = @access GROUP BY prj.ID, prj.PRJT_NAME ORDER BY prj.PRJT_NAME", new { @access = access });
            }

            return projects;
        }

        public async Task<Project> CreateProject(Project project)
        {
            Project result;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QuerySingleAsync<Project>(@"dbo.sp_createproject", param: new { @projectName = project.PRJT_NAME }, commandType: CommandType.StoredProcedure);
            }

            return project;
        }

        public async Task<IEnumerable<ProjectEvent>> GetProjectEvents(Guid projectId)
        {
            IEnumerable<ProjectEvent> projectEvents;

            using (var connection = new SqlConnection(_connectionString))
            {
                projectEvents = await connection.QueryAsync<ProjectEvent>(@"SELECT * FROM dbo.ProjectEvent WHERE PRJT_ID = @prjtID ORDER BY EVT_NAME", new { @prjtID = projectId });
            }

            return projectEvents;
        }

        public async Task<IEnumerable<ProjectEvent>> GetProjectEvents(Guid projectId, int access)
        {
            IEnumerable<ProjectEvent> projectEvents;

            using (var connection = new SqlConnection(_connectionString))
            {
                projectEvents = await connection.QueryAsync<ProjectEvent>(@"SELECT * FROM dbo.ProjectEvent WHERE PRJT_ID = @prjtID AND (EVT_ACCESS = 1 OR EVT_ACCESS & @access = @access) ORDER BY EVT_NAME", new { @prjtID = projectId, @access = access });
            }

            return projectEvents;
        }

        public async Task<ProjectEvent> CreateProjectEvent(ProjectEvent projectEvent)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PersonEventData>> GetPersonEvents(Guid personId)
        {
            IEnumerable<PersonEventData> personEvents;

            using (var connection = new SqlConnection(_connectionString))
            {
                personEvents = await connection.QueryAsync<PersonEventData>(@"SELECT pse.ID, pse.PSN_ID, pre.PRJT_ID, p.PRJT_NAME, pre.ID AS EVT_ID, pre.EVT_NAME, pre.EVT_TYPE, pse.EVT_COMMENT, pse.REG_DATE, pse.PSN_STATUS, pse.USER_ID, pse.EVT_CREATE FROM dbo.PersonEvent pse JOIN dbo.ProjectEvent pre ON pre.ID = pse.PRE_ID JOIN dbo.Project p ON p.ID = pre.PRJT_ID WHERE pse.PSN_ID = @psnId ORDER BY pse.REG_DATE DESC", new { @psnID = personId });
            }

            return personEvents;
        }

        public async Task<byte?> GetPersonLastEventStatus(Guid personId)
        {
            byte? lastStatus;

            using (var connection = new SqlConnection(_connectionString))
            {
                lastStatus = await connection.QuerySingleOrDefaultAsync<byte?>(@"SELECT TOP 1 PSN_STATUS FROM dbo.PersonEvent WHERE PSN_ID = @psnId ORDER BY REG_DATE DESC", new { @psnID = personId });
            }

            return lastStatus;
        }

        public async Task<IEnumerable<Guid>> CreateOrUpdatePersonEvents(Guid psnId, List<PersonEvent> events)
        {
            IEnumerable<Guid> eventsID;

            using (var connection = new SqlConnection(_connectionString))
            {
                foreach (var evt in events)
                {
                    switch (evt.ACTION)
                    {
                        case 0:     // создать
                            await connection.ExecuteAsync(@"INSERT INTO dbo.PersonEvent VALUES ( NEWID(), @preID, @psnID, @regDate, @psnStatus, @evtComment, @user_id, @evtCreate )",
                                new { 
                                    @preID = evt.PRE_ID, 
                                    @psnID = psnId, 
                                    @regDate = evt.REG_DATE, 
                                    @psnStatus = evt.PSN_STATUS, 
                                    @evtComment = evt.EVT_COMMENT,
                                    @user_id = UserID,
                                    @evtCreate = DateTime.Now
                                });
                            break;
                        case 1:     // обновить
                            await connection.ExecuteAsync(@"UPDATE dbo.PersonEvent SET PRE_ID = @preID, REG_DATE = @regDate, PSN_STATUS = @psnStatus, EVT_COMMENT = @evtComment WHERE ID = @id", new { @preID = evt.PRE_ID, @regDate = evt.REG_DATE, @psnStatus = evt.PSN_STATUS, @evtComment = evt.EVT_COMMENT, @id = evt.ID });
                            break;
                        case 2:     // удалить
                            await connection.ExecuteAsync(@"DELETE FROM dbo.PersonEvent WHERE ID = @id", new { @id = evt.ID });
                            break;
                    }
                }

                eventsID = await connection.QueryAsync<Guid>(@"SELECT ID FROM dbo.PersonEvent WHERE PSN_ID = @psnID", new { @psnID = psnId });
            }

            return eventsID;
        }
    }
}
