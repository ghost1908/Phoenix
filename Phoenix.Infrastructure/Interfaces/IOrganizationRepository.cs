using Phoenix.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Interfaces
{
    public interface IOrganizationRepository : IBaseRepository
    {
        Task<IEnumerable<Organization>> GetOrganizations();
        Task<int> CountPersonInOrganization(Guid orgId);
        Task<IEnumerable<Position>> GetPositions();
        Task<IEnumerable<PositionInOrganization>> GetPersonPositions(Guid psnId);
        Task<IEnumerable<Guid>> CreateOrUpdatePersonPositions(Guid psnId, List<PositionInOrganization> positions);
    }
}
