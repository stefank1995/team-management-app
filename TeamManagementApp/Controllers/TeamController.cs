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
			var user = await _userManager.GetUserAsync(User);


			// Load the teams that the signed-in user is a member of
			var teams = await _context.Teams
				.Where(team => team.Members.Any(member => member.UserId == user.Id))
				.OrderByDescending(team => team.CreatedOn)
				.ToListAsync();

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

				// Get the currently signed-in user
				var user = await _userManager.GetUserAsync(User);

				if (user != null)
				{
					// Add the current user as a member of the new team
					newTeam.Members = new[] { new UserTeam { UserId = user.Id, IsAdmin = true } };
				}

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

		//Work-in-progress
		[HttpGet]
		public IActionResult GetTeamMembers(string teamId)
		{
			// Retrieve the members of the selected team based on teamId
			var members = _context.UserTeams
				.Where(ut => ut.TeamId == teamId)
				.Select(ut => ut.AppUser)
				.ToList();

			return PartialView("_TeamMembers", members);
		}


		[HttpPost]
		public async Task<IActionResult> AssignUserToTeam(string teamId, string userEmail)
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
						team.Members = new List<UserTeam>();
					}

					// Check if the user is not already a member of the team
					if (!team.Members.Any(mt => mt.UserId == user.Id && mt.TeamId == team.Id))
					{
						var userTeam = new UserTeam
						{
							UserId = user.Id,
							TeamId = team.Id,
							IsAdmin = false // You can set this as needed
						};

						_context.UserTeams.Add(userTeam);
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
		//public async Task<IActionResult> Delete(Guid id)
		//{
		//	var team = await _context.Teams.FindAsync(id);
		//	if (team != null)
		//	{
		//		_context.Teams.Remove(team);
		//		await _context.SaveChangesAsync();

		//		return RedirectToAction("Index");
		//	}
		//	else
		//	{
		//		ModelState.AddModelError("", "No team found");
		//		return RedirectToAction("Index");
		//	}
		//}


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

