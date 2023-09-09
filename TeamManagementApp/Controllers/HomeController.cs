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
            var users = _context.Users.Select(x => x.Id).ToList();
            var assignees = _context.KanbanData.Select(y => y.AssigneeId).ToList();
            foreach (var user in users)
            {
                int intMax = _context.KanbanData.ToList().Count > 0 ? _context.KanbanData.ToList().Max(p => p.RankId) : 0;
                if (_context.KanbanData.Where(x => x.AssigneeId == user).FirstOrDefault() == null)
                {
                    KanbanData card = new KanbanData()
                    {
                        AssigneeId = user,
                        Assignee = _context.Users.Where(x => x.Id == user).FirstOrDefault().FullName,
                        AssignedBy = _context.Users.Where(x => x.Id == user).FirstOrDefault().FullName,
                        AssignedById = _context.Users.Where(x => x.Id == user).FirstOrDefault().Id,
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


        public class CRUD<T> where T : class
        {
            public object key { get; set; }

            public T value { get; set; }
        }

        [HttpPost]
        public List<KanbanData> UpdateCard([FromBody] CRUD<KanbanData> value)
        {
            var ord = value.value;
            KanbanData val = _context.KanbanData.Where(or => or.Id == ord.Id).FirstOrDefault();
            val.Status = ord.Status;
            val.AssigneeId = ord.AssigneeId;
            val.Assignee = _context.Users.Where(x => x.Id == ord.AssigneeId).FirstOrDefault().FullName;
            val.Summary = ord.Summary;
            val.AssignedById = ord.AssignedById;
            val.AssignedBy = _context.Users.Where(x => x.Id == ord.AssignedById).FirstOrDefault().FullName;
            val.Priority = ord.Priority;
            _context.SaveChanges();
            return _context.KanbanData.ToList();
        }

        [HttpPost]
        public List<KanbanData> InsertCard([FromBody] CRUD<KanbanData> value)
        {
            var data = value.value;
            int intMax = _context.KanbanData.ToList().Count > 0 ? _context.KanbanData.ToList().Max(p => p.RankId) : 0;

            KanbanData card = new KanbanData()
            {
                AssigneeId = data.AssigneeId,
                Assignee = _context.Users.Where(x => x.Id == data.AssigneeId).FirstOrDefault().FullName,
                RankId = intMax + 1,
                Status = data.Status,
                Summary = data.Summary,
                Priority = data.Priority,
                AssignedById = _userManager.GetUserAsync(User).Result.Id,
                AssignedBy = _userManager.GetUserAsync(User).Result.FullName
            };
            _context.KanbanData.Add(card);
            _context.SaveChanges();
            return _context.KanbanData.ToList();
        }
        [HttpPost]
        public List<KanbanData> RemoveCard([FromBody] CRUD<KanbanData> value)
        {
            int key = Convert.ToInt32(value.key.ToString());
            KanbanData card = _context.KanbanData.Where(c => c.RankId == key).FirstOrDefault();
            if (card != null)
            {
                _context.KanbanData.Remove(card);
                int deletedCardId = _context.KanbanData.Where(c => c.RankId == key).FirstOrDefault().RankId;
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
        public async Task<IActionResult> RemoveAllCards()
        {
            await _context.KanbanData.ExecuteDeleteAsync();
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
