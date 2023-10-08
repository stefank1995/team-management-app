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
			return View(teams);
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
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			else
			{
				foreach (var modelStateEntry in ModelState.Values)
				{
					foreach (var error in modelStateEntry.Errors)
					{
						Console.WriteLine($"Validation Error: {error.ErrorMessage}");
					}
				}
				return RedirectToAction("Index");
			}

		}


		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
			var team = await _context.Teams.FindAsync(id);
			if (team != null)
			{
				_context.Teams.Remove(team);
				await _context.SaveChangesAsync();

				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError("", "No team found");
				return RedirectToAction("Index");
			}
		}

	}
}
