using Microsoft.AspNetCore.SignalR;

namespace PublishCore.Hubs.Application.Hubs
{
    public sealed class PublishCoreHubs : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Message", $"{Context.ConnectionId} has joined");
        }

        public async Task SendMessage(string message) 
        {
            await Clients.All.SendAsync("PublishCore", message);
        }
    }
}