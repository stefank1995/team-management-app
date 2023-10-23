using Microsoft.AspNetCore.SignalR;

namespace TeamManagementApp.Hubs
{
	public class KanbanHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			Console.WriteLine($"Connection {Context.ConnectionId} made!");
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			Console.WriteLine($"{Context.ConnectionId} disconnected!");
			await base.OnDisconnectedAsync(exception);
		}
	}
}
