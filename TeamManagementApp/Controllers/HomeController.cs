using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using System.Security.Claims;
using TeamManagementApp.Data;
using TeamManagementApp.Models;

namespace TeamManagementApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public HomeController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            var kanbanData = await _context.KanbanData.ToListAsync();

            return View(kanbanData);
        }


        [HttpPost]
        public async Task<List<KanbanData>> LoadCard()
        {
            var userIds = await _context.Users.Select(u => u.Id).ToListAsync();
            var assignees = await _context.KanbanData.Select(c => c.AssigneeId).ToListAsync();

            foreach (var userId in userIds)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                int intMax = await _context.KanbanData.AnyAsync() ? await _context.KanbanData.MaxAsync(p => p.RankId) : 0;

                if (await _context.KanbanData.FirstOrDefaultAsync(c => c.AssigneeId == userId) == null)
                {
                    KanbanData card = new KanbanData()
                    {
                        AssigneeId = userId,
                        Assignee = user.FullName,
                        AssignedBy = user.FullName,
                        AssignedById = userId,
                        RankId = intMax + 1,
                        Status = "Open",
                        Summary = String.Empty,
                        Priority = "Low"
                    };

                    _context.KanbanData.Add(card);
                    await _context.SaveChangesAsync();
                }
            }
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

                List<KanbanData> updatedCards = await _context.KanbanData
                    .Where(c => c.RankId > deletedCardId)
                    .ToListAsync();

                foreach (KanbanData updatedCard in updatedCards)
                {
                    updatedCard.RankId--;
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(card));
            }

            await _context.SaveChangesAsync();
            return await _context.KanbanData.ToListAsync();
        }


        [HttpPost]
        public async Task<IActionResult> ResetBoard()
        {
            await _context.KanbanData.ExecuteDeleteAsync();
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}












