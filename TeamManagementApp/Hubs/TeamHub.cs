using Microsoft.AspNetCore.SignalR;

namespace TeamManagementApp.Hubs
{
	//Work-in-progress
	public class TeamHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			Console.WriteLine($"Connection {Context.ConnectionId} to the TeamHub made!");
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			Console.WriteLine($"{Context.ConnectionId} with the TeamHub disconnected!");
			await base.OnDisconnectedAsync(exception);
		}
		public async Task SendNotification(string userId, string message)
		{
			await Clients.User(userId).SendAsync("ReceiveNotification", message);
		}
	}
}
