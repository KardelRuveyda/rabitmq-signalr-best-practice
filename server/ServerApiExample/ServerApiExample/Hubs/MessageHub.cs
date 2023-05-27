using Microsoft.AspNetCore.SignalR;

namespace ServerApiExample.Hubs
{
    public class MessageHub:Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
