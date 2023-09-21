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

        //public UserPreferences UserPreferences { get; set; }

    }
}
