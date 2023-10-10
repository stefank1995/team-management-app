using System.ComponentModel.DataAnnotations.Schema;

namespace TeamManagementApp.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [InverseProperty(nameof(AppUser.Teams))]
        public IList<AppUser>? Members { get; set; }
        public DateTime CreatedOn { get; set; }

        public Team()
        {
            Id = Guid.NewGuid();
        }
    }
}
