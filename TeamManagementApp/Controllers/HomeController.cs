using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using TeamManagementApp.Data;
using TeamManagementApp.Models;

namespace TeamManagementApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var kanbanData = await _context.KanbanData.ToListAsync();
            return View(kanbanData);
        }

        [HttpPost]
        public List<KanbanData> LoadCard()
        {
            var users = _context.Users.Select(u => u.Id).ToList();
            var assignees = _context.KanbanData.Select(c => c.AssigneeId).ToList();
            foreach (var user in users)
            {
                int intMax = _context.KanbanData.ToList().Count > 0 ? _context.KanbanData.ToList().Max(p => p.RankId) : 0;
                if (_context.KanbanData.Where(c => c.AssigneeId == user).FirstOrDefault() == null)
                {
                    KanbanData card = new KanbanData()
                    {
                        AssigneeId = user,
                        Assignee = _context.Users.Where(u => u.Id == user).FirstOrDefault().FullName,
                        AssignedBy = _context.Users.Where(u => u.Id == user).FirstOrDefault().FullName,
                        AssignedById = _context.Users.Where(u => u.Id == user).FirstOrDefault().Id,
                        RankId = intMax + 1,
                        Status = "Open",
                        Summary = System.String.Empty,
                        Priority = "Low"
                    };

                    _context.KanbanData.Add(card);
                    _context.SaveChanges();
                }

            }
            return _context.KanbanData.ToList();
        }

        [HttpPost]
        public List<KanbanData> InsertCard([FromBody] Card<KanbanData> inputKanbanData)
        {
            var kanbanData = inputKanbanData.value;
            int intMax = _context.KanbanData.ToList().Count > 0 ? _context.KanbanData.ToList().Max(p => p.RankId) : 0;

            KanbanData card = new KanbanData()
            {
                AssigneeId = kanbanData.AssigneeId,
                Assignee = _context.Users.Where(x => x.Id == kanbanData.AssigneeId).FirstOrDefault().FullName,
                RankId = intMax + 1,
                Status = kanbanData.Status,
                Summary = kanbanData.Summary,
                Priority = kanbanData.Priority,
                AssignedById = _userManager.GetUserAsync(User).Result.Id,
                AssignedBy = _userManager.GetUserAsync(User).Result.FullName
            };
            _context.KanbanData.Add(card);
            _context.SaveChanges();
            return _context.KanbanData.ToList();
        }

        [HttpPost]
        public List<KanbanData> UpdateCard([FromBody] Card<KanbanData> inputKanbanData)
        {
            var newKanbanData = inputKanbanData.value;
            KanbanData kanbanData = _context.KanbanData.Where(c => c.Id == newKanbanData.Id).FirstOrDefault();
            kanbanData.Status = newKanbanData.Status;
            kanbanData.AssigneeId = newKanbanData.AssigneeId;
            kanbanData.Assignee = _context.Users.Where(c => c.Id == newKanbanData.AssigneeId).FirstOrDefault().FullName;
            kanbanData.Summary = newKanbanData.Summary;
            kanbanData.AssignedById = newKanbanData.AssignedById;
            kanbanData.AssignedBy = _context.Users.Where(c => c.Id == newKanbanData.AssignedById).FirstOrDefault().FullName;
            kanbanData.Priority = newKanbanData.Priority;
            _context.SaveChanges();
            return _context.KanbanData.ToList();
        }

        [HttpPost]
        public List<KanbanData> RemoveCard([FromBody] Card<KanbanData> selectedCard)
        {
            int cardNumber = Convert.ToInt32(selectedCard.key.ToString());
            KanbanData card = _context.KanbanData.Where(c => c.RankId == cardNumber).FirstOrDefault();
            if (card != null)
            {
                _context.KanbanData.Remove(card);
                int deletedCardId = _context.KanbanData.Where(c => c.RankId == cardNumber).FirstOrDefault().RankId;
                List<KanbanData> updatedCards = _context.KanbanData.Where(c => c.RankId > deletedCardId).ToList();

                foreach (KanbanData updatedCard in updatedCards)
                {
                    updatedCard.RankId--;
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(card));
            }
            _context.SaveChanges();
            return _context.KanbanData.ToList();
        }


        [HttpPost]
        public async Task<IActionResult> ResetBoard()
        {
            await _context.KanbanData.ExecuteDeleteAsync();
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
