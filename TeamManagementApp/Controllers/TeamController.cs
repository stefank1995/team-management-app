using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TeamManagementApp.Data;
using TeamManagementApp.Models;

namespace TeamManagementApp.Controllers
{
	public class TeamController : Controller
	{
		private ApplicationDbContext _context;
		private UserManager<AppUser> _userManager;

		public TeamController(ApplicationDbContext context, UserManager<AppUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var teams = await _context.Teams.ToListAsync();
			List<Team> result = new List<Team>();
			foreach (var team in teams)
			{
				result.Add(team);
			}
			return View(result);
		}


		[HttpPost]
		public async Task<IActionResult> Create([Required] Team team)
		{
			if (ModelState.IsValid)
			{
				var newTeam = new Team()
				{
					Name = team.Name,
					Description = team.Description
				};
				_context.Teams.Add(newTeam);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{
				// ModelState is not valid, so gather and log validation errors.
				foreach (var modelStateEntry in ModelState.Values)
				{
					foreach (var error in modelStateEntry.Errors)
					{
						// You can log or handle the validation error here.
						Console.WriteLine($"Validation Error: {error.ErrorMessage}");
					}
				}

				// Redirect to the same view or action to display validation errors.
				return RedirectToAction("Index");
			}

		}
	}
}
