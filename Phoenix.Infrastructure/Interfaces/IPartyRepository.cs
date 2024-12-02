using Phoenix.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Interfaces
{
    public interface IPartyRepository : IBaseRepository
    {
        Task<IEnumerable<PersonParty>> GetPersonPartyInfo(Guid personId);
        Task<IEnumerable<Guid>> CreateOrUpdatePersonPartyInfo(Guid psnId, List<PersonParty> partyInfos);
    }
}
