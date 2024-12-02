using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Phoenix.Web.Hubs
{
    public class ElectionHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
