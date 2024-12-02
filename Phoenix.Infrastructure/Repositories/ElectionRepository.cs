using Dapper;
using Phoenix.Infrastructure.Entities;
using Phoenix.Infrastructure.Entities.elc;
using Phoenix.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Repositories
{
    public class ElectionRepository : IElectionRepository
    {
        private readonly string _connectionString;

        public ElectionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Guid UserID { get; set; }

        public IEnumerable<ElectionInfo> GetElectionInfos()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CouncilItem>> GetCouncilList()
        {
            IEnumerable<CouncilItem> councils;

            using(var connection =new SqlConnection(_connectionString))
            {
                councils = await connection.QueryAsync<CouncilItem>(@"select c.ID AS CNCL_ID, c.NAME + ' ' + LOWER(ct.NAME) as CNCL_NAME, ct.WATCH_ORDER from elc.Council c join elc.CouncilType ct on ct.ID = c.CNCLT_ID order by ct.WATCH_ORDER, c.NAME + ' ' + LOWER(ct.NAME)");
            }

            return councils;
        }

        public async Task<IEnumerable<CouncilDto>> GetCouncilListForEdit()
        {
            IEnumerable<CouncilDto> councils;

            using (var connection = new SqlConnection(_connectionString))
            {
                councils = await connection.QueryAsync<CouncilDto>(@"SELECT
	                    c.ID AS CNCL_ID
	                    ,c.NAME AS CNCL_NAME
	                    ,ct.ID AS CNCL_TYPE_ID
	                    ,ct.NAME AS CNCL_TYPE_NAME
	                    ,d.ID AS DSTR_ID
	                    ,d.NUMBER AS DSTR_NUMBER
	                    ,dt.ID AS DSTR_TYPE_ID
	                    ,dt.NAME AS DSTR_TYPE_NAME
                    FROM elc.Council c
                    JOIN elc.CouncilType ct ON ct.ID = c.CNCLT_ID
                    JOIN elc.DistrictInCouncil dic ON dic.CNCL_ID = c.ID
                    JOIN ter.District d ON d.ID = dic.DSTR_ID
                    JOIN ter.DistrictType dt ON dt.ID = d.DSTRT_ID
                    ORDER BY ct.WATCH_ORDER, d.NUMBER");
            }

            return councils;
        }

        #region <--     Open election precinct              -->

        public async Task<IEnumerable<ElectionPrecinctList>> GetElectionPrecinctOpenList(Guid elcID)
        {
            IEnumerable<ElectionPrecinctList> precinctList;

            using (var connection = new SqlConnection(_connectionString))
            {
                precinctList = await connection.QueryAsync<ElectionPrecinctList>("elc.sp_getelectionprecinctlistforopen", param: new { elcID = elcID, UserID = UserID }, commandType: CommandType.StoredProcedure);
            }

            return precinctList.ToList();
        }

        public async Task<ElectionPrecinctEdit> GetElectionPrecinctEdit(Guid eprctID)
        {
            ElectionPrecinctEdit precinctEdit = new ElectionPrecinctEdit();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync("elc.sp_getelectionprecinctlistforedit", param: new { eprctID = eprctID, UserID = UserID }, commandType: CommandType.StoredProcedure))
                {
                    precinctEdit = multi.Read<ElectionPrecinctEdit>().First();
                    if (precinctEdit != null)
                        precinctEdit.BLT_RECEIVED = multi.Read<ElectionPrecinctInfoEdit>().ToList();
                }
            }

            return precinctEdit;
        }

        public async Task<string> UpdateElectionPrecinctOpen(ElectionPrecinctEdit electionPrecinct)
        {
            string result = string.Empty;

            var dtPrecinct = new DataTable();
            dtPrecinct.Columns.Add("ELC_PRCT_ID");
            dtPrecinct.Columns.Add("PRCT_OPENED");
            dtPrecinct.Columns.Add("PRCT_NOTOPENED_CAUSE");
            dtPrecinct.Columns.Add("PRCT_VOTERS");
            dtPrecinct.Rows.Add(electionPrecinct.ELC_PRCT_ID, electionPrecinct.PRCT_OPENED.Value, electionPrecinct.PRCT_NOTOPEN_CAUSE, electionPrecinct.PRCT_VOTERS);

            var dtBulletins = new DataTable();
            dtBulletins.Columns.Add("ELC_PRCT_INFO_ID");
            dtBulletins.Columns.Add("BLT_RECV");
            foreach (var blt in electionPrecinct.BLT_RECEIVED)
                dtBulletins.Rows.Add(blt.ELC_PRCT_INFO_ID, blt.BLT_RECV);

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.ExecuteScalarAsync<string>("elc.sp_updateelectionprecinctopen",
                    param: new
                    {
                        ELC_PRCT = dtPrecinct.AsTableValuedParameter("elc.TVP_ElectionPrecinct"),
                        ELC_PRCT_INFO = dtBulletins.AsTableValuedParameter("elc.TVP_ElectionPrecinctInfo")
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return result;
        }

        #endregion

        #region <--     Turnout         -->

        public async Task<IEnumerable<TurnoutPrecinctList>> GetTurnoutList(Guid elcID)
        {
            IEnumerable<TurnoutPrecinctList> turnouts;
            IEnumerable<TurnoutPrecinctTimeList> turnoutValues;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync("elc.sp_getturnoutlist", param: new { elcID = elcID, UserID = UserID }, commandType: CommandType.StoredProcedure))
                {
                    turnouts = multi.Read<TurnoutPrecinctList>().ToList();
                    turnoutValues = multi.Read<TurnoutPrecinctTimeList>().ToList();
                    foreach (var to in turnouts)
                        to.TURNOUT_VALUES = turnoutValues.Where(w => w.ELC_PRCT_ID == to.ELC_PRCT_ID).ToList();
                }
            }

            return turnouts;
        }

        public async Task<IEnumerable<TurnoutPrecinctListNK>> GetTurnoutListNK(Guid elcID)
        {
            IEnumerable<TurnoutPrecinctListNK> turnouts;
            IEnumerable<TurnoutPrecinctTimeList> turnoutValues;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync("elc.sp_getturnoutlist_nk", param: new { elcID = elcID, UserID = UserID }, commandType: CommandType.StoredProcedure))
                {
                    turnouts = multi.Read<TurnoutPrecinctListNK>().ToList();
                    turnoutValues = multi.Read<TurnoutPrecinctTimeList>().ToList();
                    foreach (var to in turnouts)
                        to.TURNOUT_VALUES = turnoutValues.Where(w => w.ELC_PRCT_ID == to.ELC_PRCT_ID).ToList();
                }
            }

            return turnouts;
        }

        public async Task<TurnoutPrecinctEdit> GetTurnoutPrecinctEdit(Guid turnoutID)
        {
            TurnoutPrecinctEdit turnoutPrecinct = new TurnoutPrecinctEdit();

            using (var connection = new SqlConnection(_connectionString))
            {
                turnoutPrecinct = await connection.QueryFirstAsync<TurnoutPrecinctEdit>("elc.sp_getturnoutprecinctforedit", param: new { turnoutID = turnoutID, UserID = UserID }, commandType: CommandType.StoredProcedure);
            }

            return turnoutPrecinct;
        }

        public async Task<string> UpdateTurnoutPrecinct(Guid turnoutId, int? turnoutVoters)
        {
            string result = string.Empty;

            var dtTurnout = new DataTable();
            dtTurnout.Columns.Add("TURNOUT_ID");
            dtTurnout.Columns.Add("TURNOUT_VOTERS");
            dtTurnout.Rows.Add(turnoutId, turnoutVoters);

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.ExecuteScalarAsync<string>("elc.sp_updateturnoutprecinct",
                    param: new { TURNOUT = dtTurnout.AsTableValuedParameter("elc.TVP_Turnout") },
                    commandType: CommandType.StoredProcedure);
            }

            return result;
        }

        #endregion

        #region <--     Protocol        -->

        public async Task<IEnumerable<ProtocolList>> GetProtocolList(Guid elcID)
        {
            IEnumerable<ProtocolList> protocols;

            using (var connection = new SqlConnection(_connectionString))
            {
                protocols = await connection.QueryAsync<ProtocolList>("elc.sp_getprotocollist", param: new { elcID = elcID, UserID = UserID }, commandType: CommandType.StoredProcedure);
            }

            return protocols;
        }

        public async Task<Protocol> GetProtocolEdit(Guid protocolID)
        {
            Protocol precinctProtocol = new Protocol();
            //IEnumerable<ProtocolItem> precinctProtocolItems;


            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync("elc.sp_getprotocolforedit", param: new { ProtocolID = protocolID, UserID = UserID }, commandType: CommandType.StoredProcedure))
                {
                    precinctProtocol = multi.ReadFirstOrDefault<Protocol>();
                    precinctProtocol.PROTOCOL_ITEMS = multi.Read<ProtocolItem>().ToList();
                }
            }

            return precinctProtocol;
        }

        public async Task<int> UpdateProtocol(Protocol protocol)
        {
            int result = int.MinValue;

            var dtProtocolItems = new DataTable();
            dtProtocolItems.Columns.Add("ID");
            dtProtocolItems.Columns.Add("VOTES");
            foreach (var item in protocol.PROTOCOL_ITEMS)
                dtProtocolItems.Rows.Add(item.PRTL_DTL_ID, item.PRTL_ITEM_VALUE);

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.ExecuteScalarAsync<int>("elc.sp_updateprotocol",
                    param: new
                    {
                        PROTOCOL_ID = protocol.PRTL_ID,
                        PROTOCOL_ISSUE = protocol.PRTL_ISSUE,
                        PROTOCOL_DATA = dtProtocolItems.AsTableValuedParameter("elc.TVP_ProtocolItemValue"),
                        UserId = UserID
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return result;
        }

        #endregion

        #region <--     Online      -->

        public async Task<IEnumerable<OnlineOpenPrecinct>> OnlineOpenPrecincts(Guid elcID)
        {
            IEnumerable<OnlineOpenPrecinct> precincts;

            using (var connection = new SqlConnection(_connectionString))
            {
                precincts = await connection.QueryAsync<OnlineOpenPrecinct>("elc.sp_onlineopenprecincts", param: new { elcID = elcID }, commandType: CommandType.StoredProcedure);
            }

            return precincts;
        }

        public async Task<IEnumerable<OnlineTurnoutPrecinct>> OnlineTurnoutPrecincts(Guid elcID)
        {
            IEnumerable<OnlineTurnoutPrecinct> precincts;

            using(var connection = new SqlConnection(_connectionString))
            {
                precincts = await connection.QueryAsync<OnlineTurnoutPrecinct>("elc.sp_onlineturnoutprecincts", param: new { elcID = elcID }, commandType: CommandType.StoredProcedure);
            }

            return precincts;
        }

        public async Task<IEnumerable<OnlineProtocolStatus>> OnlineProtocolStatus(Guid elcID)
        {
            IEnumerable<OnlineProtocolStatus> precincts;

            using (var connection = new SqlConnection(_connectionString))
            {
                precincts = await connection.QueryAsync<OnlineProtocolStatus>("elc.sp_onlineprotocol_status", param: new { elcID = elcID }, commandType: CommandType.StoredProcedure);
            }

            return precincts;
        }

        public async Task<OnlineProtocolByCouncil> GetOnlineProtocolByCouncil(Guid elcID, Guid councilID)
        {
            OnlineProtocolByCouncil protocolByCouncil = new OnlineProtocolByCouncil();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync("elc.sp_onlineprotocol_by_council", param: new { elcID = elcID, councilID = councilID }, commandType: CommandType.StoredProcedure))
                {
                    protocolByCouncil.COUNCIL_NAME = multi.ReadSingle<string>();
                    protocolByCouncil.PRTL_STATUSES = multi.Read<OnlineProtocolStatusTotal>().ToList();
                    protocolByCouncil.PRTL_ITEMS = multi.Read<OnlineProtocolItemTotal>().ToList();
                }
            }

            return protocolByCouncil;
        }

        public async Task<IEnumerable<DistrictInCouncil>> GetDistrictsByCouncil(Guid elcID, Guid councilID)
        {
            IEnumerable<DistrictInCouncil> districts;

            using (var connection = new SqlConnection(_connectionString))
            {
                districts = await connection.QueryAsync<DistrictInCouncil>("elc.sp_district_by_council", param: new { elcID = elcID, councilID = councilID }, commandType: CommandType.StoredProcedure);
            }

            return districts;
        }

        public async Task<IEnumerable<PrecinctInDistrict>> GetPrecinctByDistricts(Guid elcID, Guid districtID)
        {
            IEnumerable<PrecinctInDistrict> precincts;

            using (var connection = new SqlConnection(_connectionString))
            {
                precincts = await connection.QueryAsync<PrecinctInDistrict>("elc.sp_getprecinct_by_district", param: new { elcID = elcID, districtID = districtID }, commandType: CommandType.StoredProcedure);
            }

            return precincts;
        }

        public async Task<IEnumerable<ProtocolResultItem>> GetProtocolResult(Guid elcID, Guid councilID, Guid? districtID, Guid? precinctID)
        {
            IEnumerable<ProtocolResultItem> protocolResluts;

            using (var connection = new SqlConnection(_connectionString))
            {
                protocolResluts = await connection.QueryAsync<ProtocolResultItem>("elc.sp_onlineprotocol_by_council_or_district", param: new { elcID = elcID, councilID = councilID, districtID = districtID, precinctID = precinctID }, commandType: CommandType.StoredProcedure);
            }

            return protocolResluts;
        }

        public async Task<IEnumerable<AreaDistrictResult>> GetAreaDistrictResult()
        {
            IEnumerable<AreaDistrictResult> protocolResluts;

            using (var connection = new SqlConnection(_connectionString))
            {
                protocolResluts = await connection.QueryAsync<AreaDistrictResult>("elc.sp_getareadistrictresult", commandType: CommandType.StoredProcedure);
            }

            return protocolResluts;
        }

        public async Task<IEnumerable<ProtocolResultNK>> GetProtocolResultNK()
        {
            IEnumerable<ProtocolResultNK> protocolResluts;

            using (var connection = new SqlConnection(_connectionString))
            {
                protocolResluts = await connection.QueryAsync<ProtocolResultNK>("elc.sp_getprotocollist_nk", commandType: CommandType.StoredProcedure);
            }

            return protocolResluts;
        }

        #endregion

        public async Task<IEnumerable<PartyResultItem>> GetPartyResult(Guid elcID, Guid? communityID, Guid? precinctID)
        {
            IEnumerable<PartyResultItem> partyResults;

            using (var connection = new SqlConnection(_connectionString))
            {
                partyResults = await connection.QueryAsync<PartyResultItem>("elc.sp_get_results_community", param: new { elc_id = elcID, cmn_reg_id = communityID, prct_id = precinctID }, commandType: CommandType.StoredProcedure);
            }

            return partyResults;
        }
    }
}
