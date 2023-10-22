using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using System.Security.Claims;
using TeamManagementApp.Data;
using TeamManagementApp.Hubs;
using TeamManagementApp.Models;

namespace TeamManagementApp.Controllers
{
    //Kanban Board Controller
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<KanbanHub> _hubContext;


        public HomeController(ApplicationDbContext context, UserManager<AppUser> userManager, IHubContext<KanbanHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
        }





        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userPreference = await _context.UserPreferences.FirstOrDefaultAsync(x => x.UserId == userId);

            if (userPreference != null)
            {
                ViewBag.DarkModeEnabled = userPreference.DarkModeEnabled;
                ViewBag.SwimlanesEnabled = userPreference.SwimlanesEnabled;
            }

            //Optional code to find and remove all tasks with non-existent users(e.g. past users)
            //<------->
            var tasksWithNonExistentUsers = _context.KanbanData
                .Where(task => task.AssigneeId != null && !_context.Users.Any(u => u.Id == task.AssigneeId))
                .ToList();

            if (tasksWithNonExistentUsers.Any())
            {
                _context.KanbanData.RemoveRange(tasksWithNonExistentUsers);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveUpdate");
            }
            //<------/>
            var kanbanData = await _context.KanbanData.ToListAsync();

            return View(kanbanData);
        }




        [HttpPost]
        public async Task<List<KanbanData>> LoadCard()
        {
            //Optional code for setting default values for all the users without assignments
            //<------->
            var userIds = await _context.Users.Select(u => u.Id).ToListAsync();
            var existingAssigneeIds = await _context.KanbanData.Select(c => c.AssigneeId).ToListAsync();

            var newAssigneeIds = userIds.Except(existingAssigneeIds).ToList();

            int intMax = await _context.KanbanData.AnyAsync() ? await _context.KanbanData.MaxAsync(p => p.RankId) : 0;

            var newKanbanData = newAssigneeIds.Select(userId => new KanbanData
            {
                AssigneeId = userId,
                Assignee = _context.Users.FirstOrDefault(u => u.Id == userId)?.FullName,
                AssignedBy = _context.Users.FirstOrDefault(u => u.Id == userId)?.FullName,
                AssignedById = userId,
                RankId = intMax + 1,
                Status = "Open",
                Summary = string.Empty,
                Priority = "Low"
            });

            _context.KanbanData.AddRange(newKanbanData);
            await _context.SaveChangesAsync();
            //<------/>

            return await _context.KanbanData.ToListAsync();
        }



        [HttpPost]
        public async Task<List<KanbanData>> InsertCard([FromBody] Card<KanbanData> inputKanbanData)
        {
            var kanbanData = inputKanbanData.value;
            int intMax = await _context.KanbanData.AnyAsync() ? await _context.KanbanData.MaxAsync(p => p.RankId) : 0;

            KanbanData card = new KanbanData()
            {
                AssigneeId = kanbanData.AssigneeId,
                Assignee = (await _context.Users.FirstOrDefaultAsync(x => x.Id == kanbanData.AssigneeId))?.FullName,
                RankId = intMax + 1,
                Status = kanbanData.Status,
                Summary = kanbanData.Summary,
                Priority = kanbanData.Priority,
                AssignedById = kanbanData.AssignedById,
                AssignedBy = (await _context.Users.FirstOrDefaultAsync(x => x.Id == kanbanData.AssignedById))?.FullName
            };

            _context.KanbanData.Add(card);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("ReceiveUpdate");
            return await _context.KanbanData.ToListAsync();
        }



        [HttpPost]
        public async Task<List<KanbanData>> UpdateCard([FromBody] Card<KanbanData> inputKanbanData)
        {
            var newKanbanData = inputKanbanData.value;
            KanbanData kanbanData = await _context.KanbanData.FirstOrDefaultAsync(c => c.Id == newKanbanData.Id);

            if (kanbanData != null)
            {
                kanbanData.Status = newKanbanData.Status;
                kanbanData.AssigneeId = newKanbanData.AssigneeId;

                var assigneeUser = await _context.Users.FirstOrDefaultAsync(c => c.Id == newKanbanData.AssigneeId);
                kanbanData.Assignee = assigneeUser.FullName;

                kanbanData.Summary = newKanbanData.Summary;
                kanbanData.AssignedById = newKanbanData.AssignedById;

                var assignedByUser = await _context.Users.FirstOrDefaultAsync(c => c.Id == newKanbanData.AssignedById);
                kanbanData.AssignedBy = assignedByUser.FullName;

                kanbanData.Priority = newKanbanData.Priority;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException();
            }

            await _hubContext.Clients.All.SendAsync("ReceiveUpdate");
            return await _context.KanbanData.ToListAsync();
        }



        [HttpPost]
        public async Task<List<KanbanData>> RemoveCard([FromBody] Card<KanbanData> selectedCard)
        {
            int cardNumber = Convert.ToInt32(selectedCard.key.ToString());
            KanbanData card = await _context.KanbanData.FirstOrDefaultAsync(c => c.RankId == cardNumber);

            if (card != null)
            {
                int deletedCardId = card.RankId;
                _context.KanbanData.Remove(card);

                await _context.SaveChangesAsync();

                await _context.KanbanData
                    .Where(c => c.RankId > deletedCardId)
                    .ForEachAsync(updatedCard => updatedCard.RankId--);

                await _hubContext.Clients.All.SendAsync("ReceiveUpdate");
                return await _context.KanbanData.ToListAsync();
            }
            else
            {
                throw new ArgumentNullException(nameof(card));
            }
        }




        [HttpPost]
        public async Task<IActionResult> ResetBoard()
        {
            await _context.KanbanData.ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveUpdate");
            return RedirectToAction("Index");
        }

    }
}
