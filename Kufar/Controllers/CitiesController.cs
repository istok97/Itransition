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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Kufar.Controllers
{
    public class CitiesController : Controller
    {
        private readonly AdvertisementDbContext _context;

        private readonly ILogger<AdvertisementsController> _logger;

        public CitiesController(AdvertisementDbContext context, ILogger<AdvertisementsController> logger)
        {
            _context = context;

            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
          
            var result = _context.Cities.Where(city => city.Country != null && city.Country.Id == id).ToList();
            _logger.LogInformation("Юзер запросил создание города для страны у которой id == " + id);

            var citiesViewModel = new CitiesViewModel
            {
                SelectedCountry = _context.Countries.SingleOrDefault(x => x.Id == id),
                Cities = result
            };
               _logger.LogInformation("Юзер запросил создание города для страны у которой id == " + id);
            return View(citiesViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name")] CitiesViewModel citiesViewModel)
        {
            if (ModelState.IsValid)
            {
                var cities = new City
                {
                    Name = citiesViewModel.Name,
                    Country = _context.Countries.SingleOrDefault(x => x.Id == citiesViewModel.Id)
                };
             
                _context.Add(cities);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Countries");
            }

            return View(citiesViewModel);
        }       
    }
}
