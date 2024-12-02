using Phoenix.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Interfaces
{
    public interface IReportRepository : IBaseRepository
    {
        Task<IEnumerable<CreatedPersons>> GetCreatedPersons(DateTime startDate, DateTime endDate);
        Task<IEnumerable<CommunityPlans>> GetCommunityPlans(DateTime startDate, DateTime endDate);
        Task<IEnumerable<PersonState>> GetPersonState(Guid? areaId, Guid? regionId, Guid? communityId);

        Task<IEnumerable<Person>> GetPersonByBirthday(DateTime? birthDay);
    }
}
