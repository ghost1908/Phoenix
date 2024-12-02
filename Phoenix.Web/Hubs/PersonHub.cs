using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Phoenix.Infrastructure.Interfaces;
using Phoenix.Web.Models.Report;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Hubs
{
    [Authorize]
    public class PersonHub : Hub
    {
        private readonly IHubRepository _hubRepository;

        public PersonHub(IHubRepository hubRepository)
        {
            _hubRepository = hubRepository;
        }

        public async Task PersonEditing(string personId)
        {
            var userClaim = Context.User.FindFirst("DisplayName");
            string userDisplayName = userClaim == null ? (Context.User.Identity.Name ?? "unknown") : userClaim.Value;

            await _hubRepository.PersonEditingAsync(Context.ConnectionId, personId, userDisplayName);

            var persons = await _hubRepository.GetEditingPersonsAsync();
            var model = persons.Select(s => new PersonHubViewModel()
            {
                PersonId = s.PSN_ID.HasValue ? s.PSN_ID.Value.ToString().ToLower() : string.Empty,
                UserName = s.USR_DSPL_NAME,
                Style = "background-color: #ffe200;"
            }).ToList();

            await Clients.Others.SendAsync("Notify", model);
        }

        public override async Task OnConnectedAsync()
        {
            //var userClaim = Context.User.FindFirst("DisplayName");
            //string userDisplayName = userClaim == null ? (Context.User.Identity.Name ?? "unknown" ): userClaim.Value;

            //await _hubRepository.ConnectAsync(Context.ConnectionId, userDisplayName);
            var persons = await _hubRepository.GetEditingPersonsAsync();
            var model = persons.Select(s => new PersonHubViewModel()
            {
                PersonId = s.PSN_ID.HasValue ? s.PSN_ID.Value.ToString().ToLower() : string.Empty,
                UserName = s.USR_DSPL_NAME,
                Style = "background-color: #ffe200;"
            }).ToList();

            await Clients.All.SendAsync("Notify", model);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var persons = await _hubRepository.DisconnectAsync(Context.ConnectionId);
            var model = persons.Select(s => new PersonHubViewModel()
            {
                PersonId = s.PSN_ID.HasValue ? s.PSN_ID.Value.ToString().ToLower() : string.Empty,
                UserName = s.USR_DSPL_NAME,
                Style = string.Empty
            }).ToList();

            await Clients.Others.SendAsync("Notify", model);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
