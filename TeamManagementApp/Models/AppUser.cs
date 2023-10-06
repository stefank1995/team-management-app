using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

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

        [InverseProperty(nameof(Team.Members))]
        public IList<Team> Teams { get; set; }
        public UserPreferences UserPreferences { get; set; }
        public AppUser() : base()
        {
            UserPreferences = new UserPreferences();
        }

    }
}
