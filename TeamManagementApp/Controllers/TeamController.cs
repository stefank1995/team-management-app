using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TeamManagementApp.Data;
using TeamManagementApp.Models;

namespace TeamManagementApp.Controllers
{
	//Work-in-progress
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
			var teams = await _context.Teams.OrderByDescending(team => team.CreatedOn).ToListAsync();
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
					Description = team.Description,
					CreatedOn = DateTime.Now

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

		[HttpPost]
		public async Task<IActionResult> AssignUserToTeam(Guid teamId, string userEmail)
		{
			var team = await _context.Teams.FindAsync(teamId);

			if (team != null)
			{
				var user = await _userManager.FindByEmailAsync(userEmail);
				if (user != null)
				{
					// Check if the user is not already a member of the team to avoid duplication
					if (team.Members == null)
					{
						team.Members = new List<AppUser>();
					}

					if (!team.Members.Contains(user))
					{
						team.Members.Add(user);
						await _context.SaveChangesAsync();
						return RedirectToAction("Index");
					}
					else
					{
						ModelState.AddModelError("", "User is already a member of the team.");
					}
				}
				else
				{
					ModelState.AddModelError("", "User not found. Please check the email address.");
				}
			}
			else
			{
				ModelState.AddModelError("", "Team not found.");
			}

			return RedirectToAction("Index");
		}


		//[HttpPost]
		//public async Task<IActionResult> RemoveMember(Guid userId, Guid teamId)
		//{
		//	var user = await _context.Users.FindAsync(userId);
		//	var team = await _context.Teams.FindAsync(teamId);
		//	if (user != null)
		//	{
		//		var teamMembership = await _context.TeamMemberships
		//			.FirstOrDefaultAsync(tm => tm.TeamId == teamId && tm.UserId == userId);

		//		if (teamMembership != null)
		//		{
		//			_context.TeamMemberships.Remove(teamMembership);
		//			await _context.SaveChangesAsync();
		//		}
		//	}
		//	return RedirectToAction("Index");
		//}




	}
}
