using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kufar.Models;
using System.Linq;
using System.Threading.Tasks;
namespace Kufar.Controllers
{
    public class HomeController : Controller
    {
        AdvertisementDbContext db;
        public HomeController(AdvertisementDbContext context)
        {
            this.db = context;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
