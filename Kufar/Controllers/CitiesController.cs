using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kufar.Models;
using Kufar.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Kufar.Controllers
{
    public class CitiesController : Controller
    {
        private readonly AdvertisementDbContext _context;

        public CitiesController(AdvertisementDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Create(int id)
        {
          
            var result = _context.Cities.Where(city => city.Country != null && city.Country.Id == id).ToList();


            var cities = new CitiesViewModel
            {
                SelectedCountry = _context.Countries.SingleOrDefault(x => x.Id == id),
                Cities = result
            };

            return View(cities);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, CountryId")] CitiesViewModel citiesViewModel)
        {
            if (ModelState.IsValid)
            {
                var cities = new City
                {
                    Id = citiesViewModel.Id,
                    Name = citiesViewModel.Name,
                    Country = _context.Countries.SingleOrDefault(x => x.Id == citiesViewModel.CountryId),
                };
                _context.Add(cities);
                await _context.SaveChangesAsync();
                return View("Create");
            }
            return View("Create");
        }
      

      

       
    }
}
