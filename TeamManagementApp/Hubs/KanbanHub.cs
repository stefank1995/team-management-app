using Microsoft.AspNetCore.SignalR;

namespace TeamManagementApp.Hubs
{
	public class KanbanHub : Hub
	{
		public async Task UpdateKanbanBoard()
		{
			await Clients.All.SendAsync("ReceiveUpdate");
		}
	}
}
