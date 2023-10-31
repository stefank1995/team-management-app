using System.ComponentModel.DataAnnotations;

namespace TeamManagementApp.Models
{
	public class UserTeam
	{
		[Key]
		public string UserId { get; set; }

		[Key]
		public string TeamId { get; set; }

		public bool IsAdmin { get; set; }

		// Navigation properties
		public AppUser AppUser { get; set; }
		public Team Team { get; set; }
	}
}
