using Phoenix.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Interfaces
{
    public interface IPersonRepository : IBaseRepository
    {
        Task<PersonList> GetPeopleList(PersonFilter filter, int skip, int take);
        Task<int> GetPeopleListCount();
        Task<byte> GetLastFormStatus(Guid personId);
        Task<Person> GetPerson(Guid personID);
        Task<PersonInfo> GetPersonInfo(Guid personID);

        Task<Person> CreatePerson(Person person);
        Task<Person> UpdatePerson(Person person);

        Task<IEnumerable<Person>> FindDublicates(string lastName, string firstName);

        Task<PersonInfo> UpdatePersonInfo(PersonInfo personInfo);

        Task<IEnumerable<PersonFormStatus>> GetPersonFormStatuses(Guid personId);
        Task<IEnumerable<Guid>> CreateOrUpdateFormStatuses(Guid psnId, List<PersonFormStatus> statuses);
    }
}
