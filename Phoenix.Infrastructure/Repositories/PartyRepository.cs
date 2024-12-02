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
    public class PartyRepository : IPartyRepository
    {
        private readonly string _connectionString;
        private LogChangeService _logService;

        public Guid UserID { get; set; }

        public PartyRepository(string connectionString)
        {
            _connectionString = connectionString;
            _logService = new LogChangeService(_connectionString);
        }

        public async Task<IEnumerable<PersonParty>> GetPersonPartyInfo(Guid personId)
        {
            IEnumerable<PersonParty> personPartyInfo;

            using (var connection = new SqlConnection(_connectionString))
            {
                personPartyInfo = await connection.QueryAsync<PersonParty>(@"SELECT * FROM dbo.PersonParty WHERE PSN_ID = @psnId", new { @psnId = personId });
            }

            return personPartyInfo;
        }

        public async Task<IEnumerable<Guid>> CreateOrUpdatePersonPartyInfo(Guid psnId, List<PersonParty> partyInfos)
        {
            IEnumerable<Guid> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                foreach (var item in partyInfos)
                {
                    DateTime logDate = DateTime.Now;
                    switch (item.ACTION)
                    {
                        case 0:     // создать
                            await connection.ExecuteAsync(@"INSERT INTO dbo.PersonParty VALUES ( NEWID(), @psnId, @tNumber, @eDate, @aDate, @aNumber, @aComment, @dDate, @dNumber, @dCause, @dComment, @userId, @cDate )",
                                new
                                {
                                    @psnID = psnId,
                                    @tNumber = item.TICKET_NUMBER,
                                    @eDate = item.DATE_ENTRY,
                                    @aDate = item.DATE_ADOPTION,
                                    @aNumber = item.ADOPTION_NUMBER,
                                    @aComment = item.ADOPTION_COMMENT,
                                    @dDate = item.DATE_DISPOSAL,
                                    @dNumber = item.DISPOSAL_NUMBER,
                                    @dCause = item.DISPOSAL_CAUSE,
                                    @dComment = item.DISPOSAL_COMMENT,
                                    @userId = UserID,
                                    @cDate = logDate
                                });
                            break;
                        case 1:     // обновить
                            var oldItem = await connection.QueryFirstAsync<PersonParty>(@"SELECT * FROM dbo.PersonParty WHERE ID = @id", new { @id = item.ID });
                            await connection.ExecuteAsync(@"UPDATE dbo.PersonParty SET TICKET_NUMBER = @tNumber, DATE_ENTRY = @eDate, DATE_ADOPTION = @aDate, ADOPTION_NUMBER = @aNumber, ADOPTION_COMMENT = @aComment, DATE_DISPOSAL = @dDate, DISPOSAL_NUMBER = @dNumber, DISPOSAL_CAUSE = @dCause, DISPOSAL_COMMENT = @dComment WHERE ID = @id",
                                new
                                {
                                    @id = item.ID,
                                    @tNumber = item.TICKET_NUMBER,
                                    @eDate = item.DATE_ENTRY,
                                    @aDate = item.DATE_ADOPTION,
                                    @aNumber = item.ADOPTION_NUMBER,
                                    @aComment = item.ADOPTION_COMMENT,
                                    @dDate = item.DATE_DISPOSAL,
                                    @dNumber = item.DISPOSAL_NUMBER,
                                    @dCause = item.DISPOSAL_CAUSE,
                                    @dComment = item.DISPOSAL_COMMENT
                                });

                            if (oldItem.TICKET_NUMBER != item.TICKET_NUMBER)
                                await _logService.WriteLog(connection, "dbo.PersonParty", item.ID, "TICKET_NUMBER", oldItem.TICKET_NUMBER, item.TICKET_NUMBER, UserID, logDate);

                            if (oldItem.DATE_ENTRY != item.DATE_ENTRY)
                                await _logService.WriteLog(connection, "dbo.PersonParty", item.ID, "DATE_ENTRY", oldItem.DATE_ENTRY, item.DATE_ENTRY, UserID, logDate);

                            break;
                        case 2:     // удалить
                            //await connection.ExecuteAsync(@"DELETE FROM dbo.PersonEvent WHERE ID = @id", new { @id = evt.ID });
                            break;
                    }
                }

                result = await connection.QueryAsync<Guid>(@"SELECT ID FROM dbo.PersonParty WHERE PSN_ID = @psnID", new { @psnID = psnId });
            }

            return result;
        }
    }
}
