using Phoenix.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Interfaces
{
    public interface IHubRepository : IBaseRepository
    {
        Task ConnectAsync(string connectionId, string userDisplayName);
        Task<IEnumerable<HubPerson>> DisconnectAsync(string connectionId);
        Task<IEnumerable<HubPerson>> GetEditingPersonsAsync();
        //Task PersonEditingAsync(string connectionId, string personId);
        Task PersonEditingAsync(string connectionId, string personId, string userDisplayName);
        Task<string> GetDisplayNameAsync(string personId);
    }
}
