using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kufar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Kufar.ViewModels;
using Kufar.Services;

namespace Kufar.Controllers
{
    [Authorize]
    public class CountriesController : Controller
    {
        private readonly AdvertisementDbContext _context;

        private readonly ICountryService countryService;

        public CountriesController(AdvertisementDbContext context, ICountryService countryService)
        {
            this.countryService = countryService;
            _context = context;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries.ToListAsync());
        }
        [Authorize]
        public IActionResult Create()
        {
            return PartialView();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(country);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = await _context.Countries
           .SingleOrDefaultAsync(m => m.Id == id);
            DeleteCityViewModel model = new DeleteCityViewModel()
            {
                Id = country.Id,
                Name = country.Name,
                
            };
          
          

            return PartialView(model);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id, DeleteCityViewModel deleteCityViewModel)
        {

            await countryService.DeleteCountryAsync(Id);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<JsonResult> Countries()
        {
            var countries = await _context.Countries.ToListAsync();
            return Json(countries);
        }

    }
}
