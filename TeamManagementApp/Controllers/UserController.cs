﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManagementApp.Data;
using TeamManagementApp.Models;
using TeamManagementApp.ViewModels;

namespace TeamManagementApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public UserController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = _userManager.GetRolesAsync(user).Result.ToList().FirstOrDefault()
                };
                result.Add(userViewModel);
            }
            return View("~/Views/Settings/User/UserManagement.cshtml", result);
        }
    }
}
