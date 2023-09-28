namespace TeamManagementApp.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<AppUser>? Members { get; set; }
    }
}
