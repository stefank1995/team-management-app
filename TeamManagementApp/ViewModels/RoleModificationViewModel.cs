using Microsoft.AspNetCore.Identity;
using TeamManagementApp.Models;

namespace TeamManagementApp.ViewModels
{
    public class RoleModificationViewModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }
}
