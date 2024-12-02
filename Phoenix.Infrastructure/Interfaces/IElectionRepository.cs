using Phoenix.Infrastructure.Entities;
using Phoenix.Infrastructure.Entities.elc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Interfaces
{
    public interface IElectionRepository : IBaseRepository
    {
        IEnumerable<ElectionInfo> GetElectionInfos();
        Task<IEnumerable<CouncilItem>> GetCouncilList();

        Task<IEnumerable<CouncilDto>> GetCouncilListForEdit();

        Task<IEnumerable<ElectionPrecinctList>> GetElectionPrecinctOpenList(Guid elcID);
        Task<ElectionPrecinctEdit> GetElectionPrecinctEdit(Guid eprctID);
        Task<string> UpdateElectionPrecinctOpen(ElectionPrecinctEdit electionPrecinct);

        Task<IEnumerable<TurnoutPrecinctList>> GetTurnoutList(Guid elcID);
        Task<TurnoutPrecinctEdit> GetTurnoutPrecinctEdit(Guid turnoutID);
        Task<string> UpdateTurnoutPrecinct(Guid turnoutId, int? turnoutVoters);
        Task<IEnumerable<TurnoutPrecinctListNK>> GetTurnoutListNK(Guid elcID);

        Task<Protocol> GetProtocolEdit(Guid protocolID);
        Task<IEnumerable<ProtocolList>> GetProtocolList(Guid elcID);
        Task<int> UpdateProtocol(Protocol protocol);

        Task<IEnumerable<OnlineOpenPrecinct>> OnlineOpenPrecincts(Guid elcID);
        Task<IEnumerable<OnlineTurnoutPrecinct>> OnlineTurnoutPrecincts(Guid elcID);
        Task<IEnumerable<OnlineProtocolStatus>> OnlineProtocolStatus(Guid elcID);
        Task<OnlineProtocolByCouncil> GetOnlineProtocolByCouncil(Guid elcID, Guid councilID);

        Task<IEnumerable<DistrictInCouncil>> GetDistrictsByCouncil(Guid elcID, Guid councilID);
        Task<IEnumerable<PrecinctInDistrict>> GetPrecinctByDistricts(Guid elcID, Guid districtID);
        Task<IEnumerable<ProtocolResultItem>> GetProtocolResult(Guid elcID, Guid councilID, Guid? districtID, Guid? precinctID);

        Task<IEnumerable<PartyResultItem>> GetPartyResult(Guid elcID, Guid? communityID, Guid? precinctID);

        Task<IEnumerable<AreaDistrictResult>> GetAreaDistrictResult();
        Task<IEnumerable<ProtocolResultNK>> GetProtocolResultNK();
    }
}
