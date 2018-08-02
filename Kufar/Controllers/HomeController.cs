using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kufar.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kufar.Controllers
{
    public class HomeController : Controller
    {
        private readonly AdvertisementDbContext _context;

        public ActionResult Index()
        {
            return View();
        }
     

    }

}
