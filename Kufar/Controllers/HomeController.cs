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
        public ActionResult Index()
        {
            CountryModel objcountrymodel = new CountryModel();
            objcountrymodel.StateModel = new List<State>();
            objcountrymodel.StateModel = GetAllState();
            return View(objcountrymodel);
        }

        [HttpPost]
        public ActionResult GetCityByStaeId(int stateid)
        {
            List<City> objcity = new List<City>();
         //   objcity = GetAllCity().Where(m => m.StateId == stateid).ToList();
            SelectList obgcity = new SelectList(objcity, "Id", "CityName", 0);
            return Json(obgcity);
        }

        public List<State> GetAllState()
        {
            List<State> objstate = new List<State>();
            objstate.Add(new State { Id = 0, StateName = "Select State" });
            objstate.Add(new State { Id = 1, StateName = "Минская область" });
            objstate.Add(new State { Id = 2, StateName = "Гродненская область" });
            objstate.Add(new State { Id = 3, StateName = "Витебская область" });
            objstate.Add(new State { Id = 4, StateName = "Гомельская область" });
            objstate.Add(new State { Id = 5, StateName = "Брестская область" });
            objstate.Add(new State { Id = 6, StateName = "Могилевская область" });
            return objstate;
        }

        public List<City> GetAllCity()
        {
            List<City> objcity = new List<City>();
            //objcity.Add(new City { Id = 1, StateId = 1, CityName = "Minsk" });
            //objcity.Add(new City { Id = 2, StateId = 2, CityName = "Гродно" });
            //objcity.Add(new City { Id = 3, StateId = 3, CityName = "Витебск" });
            //objcity.Add(new City { Id = 4, StateId = 4, CityName = "Гомель" });
            //objcity.Add(new City { Id = 5, StateId = 5, CityName = "Брест" });
            //objcity.Add(new City { Id = 6, StateId = 6, CityName = "Могилев" });
            //objcity.Add(new City { Id = 7, StateId = 1, CityName = "Молодечно" });
            return objcity;
        }
    }

}
