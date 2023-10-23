using Microsoft.AspNetCore.SignalR;

namespace TeamManagementApp.Hubs
{
    //Work-in-progress
    public class TeamHub : Hub
    {
        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
