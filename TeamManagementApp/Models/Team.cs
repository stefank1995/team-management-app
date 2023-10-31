namespace TeamManagementApp.Models
{
	public class Team
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public ICollection<UserTeam>? Members { get; set; }
		public DateTime CreatedOn { get; set; }

		public Team()
		{
			Id = Guid.NewGuid().ToString();
		}
	}
}
