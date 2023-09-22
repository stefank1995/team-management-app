using System.ComponentModel.DataAnnotations.Schema;

namespace TeamManagementApp.Models
{
    public class UserPreferences
    {
        public string Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public bool NightModeEnabled { get; set; } = false;
        public bool SwimlanesEnabled { get; set; } = true;

        public UserPreferences()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
