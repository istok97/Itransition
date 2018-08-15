using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kufar.Models;
using Kufar.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
namespace Kufar.Controllers
{
    public class UserController : Controller
    {
        readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public ViewResult Index()
        {
            ViewBag.userManager = _userManager;
            return View(_userManager.Users.ToList());
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new UserViewModel
            {
                Email = user.Email,
                Year = user.Year
            };
            return View(model);
        }
    }
}