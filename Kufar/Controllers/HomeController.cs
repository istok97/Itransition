using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kufar.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Kufar.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }
        public ActionResult Index()
        {
            ViewData["Title"] = _localizer["Header"];
            ViewData["Message"] = _localizer["Message"];
            return View();
        }
     

    }

}
