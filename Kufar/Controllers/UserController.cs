using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kufar.Models;
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
        public ViewResult Index()
        {
            ViewBag.userManager = _userManager;
            return View(_userManager.Users.ToList());
        }

    }
}