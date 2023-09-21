using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamManagementApp.Data;
using TeamManagementApp.Models;

namespace TeamManagementApp.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public SettingsController(ApplicationDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePreferences(UserPreferences userPreferences)
        {
            // Get the current user's identity
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve the user's existing preferences from the database
            var user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                // Update user preferences based on the received data
                user.UserPreferences.NightModeEnabled = userPreferences.NightModeEnabled;
                user.UserPreferences.SwimlanesEnabled = userPreferences.SwimlanesEnabled;

                _context.Users.Update(user);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }




        //private async Task<UserPreferences> LoadAllUserPreferences()
        //{

        //    return await _context.UserPreferences.ToListAsync();
        //}







    }
}
