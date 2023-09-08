﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Base;
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
        public List<KanbanData> LoadCard([FromBody] ExtendedDataManagerRequest param)
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



        public class ExtendedDataManagerRequest : DataManagerRequest     //inherit the class to show age as property of DataManager 
        {
            public string NewName { get; set; }
        }

        public class ICRUDModel<T> where T : class
        {
            public string action { get; set; }

            public string table { get; set; }

            public string keyColumn { get; set; }

            public object key { get; set; }

            public T value { get; set; }

            public List<T> added { get; set; }

            public List<T> changed { get; set; }

            public List<T> deleted { get; set; }

            public IDictionary<string, object> @params { get; set; }
        }

        [HttpPost]
        public List<KanbanData> UpdateCard([FromBody] ICRUDModel<KanbanData> value)
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
        public List<KanbanData> InsertCard([FromBody] ICRUDModel<KanbanData> value)
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
        public List<KanbanData> RemoveCard([FromBody] ICRUDModel<KanbanData> value)
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
        public List<KanbanData> CrudCard([FromBody] ICRUDModel<KanbanData> param)
        {
            // this block of code will execute while inserting the new cards
            if (param.action == "insert" || (param.action == "batch" && param.added.Count > 0))
            {
                var value = (param.action == "insert") ? param.value : param.added[0];
                int intMax = _context.KanbanData.ToList().Count > 0 ? _context.KanbanData.ToList().Max(p => p.RankId) : 0;
                KanbanData card = new KanbanData()
                {
                    AssigneeId = value.AssigneeId,
                    Assignee = _context.Users.Where(x => x.Id == value.AssigneeId).FirstOrDefault().FullName,
                    RankId = intMax + 1,
                    Status = value.Status,
                    Summary = value.Summary,
                    Priority = value.Priority,
                    AssignedById = _userManager.GetUserAsync(User).Result.Id,
                    AssignedBy = _userManager.GetUserAsync(User).Result.FullName

                };
                _context.KanbanData.Add(card);
                _context.SaveChanges();


            }
            // this block of code will execute while updating the existing cards
            if (param.action == "update" || (param.action == "batch" && param.changed.Count > 0))
            {
                KanbanData value = (param.action == "update") ? param.value : param.changed[0];
                IQueryable<KanbanData> filterData = _context.KanbanData.Where(c => c.Id == value.Id);
                if (filterData.Count() > 0)
                {
                    KanbanData card = _context.KanbanData.Single(A => A.Id == value.Id);
                    card.Summary = value.Summary;
                    card.AssigneeId = value.AssigneeId;
                    card.Assignee = _context.Users.Where(x => x.Id == value.AssigneeId).FirstOrDefault().FullName;
                    card.Status = value.Status;
                    card.AssignedById = value.AssignedById;
                    card.AssignedBy = _context.Users.Where(x => x.Id == value.AssignedById).FirstOrDefault().FullName;
                    card.Priority = value.Priority;
                }
                _context.SaveChanges();
            }
            // this block of code will execute while deleting the existing cards
            if (param.action == "remove" || (param.action == "batch" && param.deleted.Count > 0))
            {
                if (param.action == "remove")
                {
                    int key = Convert.ToInt32(param.key.ToString());
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
                }
                else
                {
                    foreach (KanbanData cards in param.deleted)
                    {
                        string key = param.key.ToString();
                        KanbanData card = _context.KanbanData.Where(c => c.Id == cards.Id).FirstOrDefault();
                        if (cards != null)
                        {
                            _context.KanbanData.Remove(card);
                            int deletedCardId = _context.KanbanData.Where(c => c.Id == key).FirstOrDefault().RankId;
                            List<KanbanData> updatedCards = _context.KanbanData.Where(c => c.RankId > deletedCardId).ToList();

                            foreach (KanbanData updatedCard in updatedCards)
                            {
                                updatedCard.RankId--;
                            }
                        }
                    }
                }
                _context.SaveChanges();
            }
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
