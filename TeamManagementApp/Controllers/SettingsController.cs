using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userPreference = _context.UserPreferences.FirstOrDefault(x => x.UserId == userId);
            if (userPreference != null)
            {
                ViewBag.DarkModeEnabled = userPreference.DarkModeEnabled;
                ViewBag.SwimlanesEnabled = userPreference.SwimlanesEnabled;
            }
            else
            {
                ViewBag.DarkModeEnabled = false;
                ViewBag.SwimlanesEnabled = true;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePreferences(UserPreferences userPreference)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(); // Return 401 Unauthorized if user is not authenticated
                }
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return NotFound(); // Return 404 Not Found if user is not found
                }

                UserPreferences userPreferences = _context.UserPreferences.First(u => u.UserId == userId);
                userPreferences.DarkModeEnabled = userPreference.DarkModeEnabled;
                userPreferences.SwimlanesEnabled = userPreference.SwimlanesEnabled;
                _context.UserPreferences.Update(userPreferences);
                await _context.SaveChangesAsync();


                return Ok(userPreferences);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.Error.WriteLine(ex);

                // Return a 500 Internal Server Error response
                return StatusCode(500, "Failed to update preferences. Please try again later.");
            }
        }


    }
}
