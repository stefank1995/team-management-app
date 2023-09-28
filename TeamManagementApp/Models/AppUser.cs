using Microsoft.AspNetCore.Identity;

namespace TeamManagementApp.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public List<Team>? Teams { get; set; }
        public UserPreferences UserPreferences { get; set; }
        public AppUser() : base()
        {
            UserPreferences = new UserPreferences();
        }

    }
}
