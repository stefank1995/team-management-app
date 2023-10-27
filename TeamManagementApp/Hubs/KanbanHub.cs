using Microsoft.AspNetCore.SignalR;

namespace TeamManagementApp.Hubs
{
	//Work-in-progress
	public class KanbanHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			Console.WriteLine($"Connection {Context.ConnectionId} to the KanbanHub made!");
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			Console.WriteLine($"{Context.ConnectionId} with the KanbanHub disconnected!");
			await base.OnDisconnectedAsync(exception);
		}
	}
}
