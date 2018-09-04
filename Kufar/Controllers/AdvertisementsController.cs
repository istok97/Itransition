using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kufar.Models;
using Kufar.Services;
using Kufar.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kufar.Controllers
{

    public class AdvertisementsController : Controller
    {
        private readonly AdvertisementDbContext _context;

        private readonly IAdvertisementsService _advertisementsService;

        private List<City> _list;

        public AdvertisementsController(AdvertisementDbContext context, IAdvertisementsService advertisementsService)
        {
            _context = context;
            _advertisementsService = advertisementsService;
        }

        [HttpPost]
        public IActionResult GetSettings(string changeview, string returnUrl)
        {
            //Response.Cookies.Append(
            //    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return LocalRedirect(returnUrl);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3, SortType sortOrder = SortType.TitleAsc, int viewType = 0)
        {
            IQueryable<Advertisement> advertisements =
                _context.Advertisements.Include(x => x.Country).Include(x => x.City);
            
            switch (sortOrder)
            {
                case SortType.TitleDesc:
                    advertisements = advertisements.OrderByDescending(s => s.Title);
                    break;
                case SortType.DescriptionAsc:
                    advertisements = advertisements.OrderBy(s => s.Description);
                    break;
                case SortType.DescriptionDesc:
                    advertisements = advertisements.OrderByDescending(s => s.Description);
                    break;

                default:
                    advertisements = advertisements.OrderBy(s => s.Title);
                    break;
            }

            var count = await advertisements.CountAsync();
            var items = await advertisements.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexViewModel viewModel = new IndexViewModel
            {
                ViewType = viewType,
                PageViewModel = new PageViewModel(count, pageNumber, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                Advertisements = items
            };

            return View(viewModel);
        }

        public async Task<ActionResult> ChangePartial(int Id, int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Advertisement> advertisements =
               _context.Advertisements.Include(x => x.Country).Include(x => x.City);
            var items = await advertisements.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexViewModel viewModel = new IndexViewModel
            {

              
                Advertisements = items
            };

            if (Id == 1)
            {
                return PartialView("_Block", viewModel);
            }
            return PartialView("_list", viewModel);
        }
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .SingleOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        public IActionResult Create()
        {
            ViewBag.countries = _context.Countries.ToList();
            return View("Create");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Title, Description, Photo, CountryId, CityId")] AdvertisementViewModel advertisementViewModel)
        {

            if (ModelState.IsValid)
            {
                var adv4 = new Advertisement
                {
                    Id = advertisementViewModel.Id,
                    Title = advertisementViewModel.Title,
                    Description = advertisementViewModel.Description,
                    Photo = advertisementViewModel.Photo,
                    Country = _context.Countries.SingleOrDefault(x => x.Id == advertisementViewModel.CountryId),
                    City = _context.Cities.SingleOrDefault(x => x.Id == advertisementViewModel.CityId)
                };

                //
                _context.Add(adv4);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.countries = _context.Countries.ToList();
            return View(advertisementViewModel);
        }

        [Authorize]
        public JsonResult GetStateById(int id)
        {

            _list = _context.Cities.Where(a => a.Country.Id == id).ToList();
            _list.Insert(0, new City { Id = 0, Name = "Select City" });
            return Json(new SelectList(_list, "Id", "Name"));
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Advertisement editadAdvertisement = _context.Advertisements.Include(a => a.City).Include(a => a.Country).Single(a => a.Id == id);
            AdvertisementViewModel model = new AdvertisementViewModel()
            {
                Id = editadAdvertisement.Id,
                Title = editadAdvertisement.Title,
                Description = editadAdvertisement.Description,
                Photo = editadAdvertisement.Photo,
                CityId = editadAdvertisement.City?.Id ?? 0,
                CountryId = editadAdvertisement.Country?.Id ?? 0
            };

            IQueryable<SelectListItem> countries;
            SelectListItem selectedCountry;

            if (model.CountryId == 0)
            {
                countries = _context.Countries.Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString(), Selected = t.Id == model.CountryId });
                selectedCountry = countries.First();

            }

            else
            {
                countries = _context.Countries.Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString(), Selected = t.Id == model.CountryId });
                selectedCountry = countries.Single(x => x.Selected);
            }

            int selectedCOuntryId = int.Parse(selectedCountry.Value);
            ViewBag.countries = countries;
            ViewBag.cities = _context.Cities.Where(c => c.Country.Id == selectedCOuntryId).Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString(), Selected = t.Id == model.CityId });

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Photo, CountryId, CityId")] AdvertisementViewModel advertisementViewModel)
        {
            if (id != advertisementViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var adv4 = new Advertisement
                {
                    Id = advertisementViewModel.Id,
                    Title = advertisementViewModel.Title,
                    Description = advertisementViewModel.Description,
                    Photo = advertisementViewModel.Photo,
                    Country = _context.Countries.SingleOrDefault(x => x.Id == advertisementViewModel.CountryId),
                    City = _context.Cities.SingleOrDefault(x => x.Id == advertisementViewModel.CityId)
                };
                try
                {
                    _context.Update(adv4);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(adv4.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(advertisementViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .SingleOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisements.SingleOrDefaultAsync(m => m.Id == id);
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool AdvertisementExists(int id)
        {
            return _context.Advertisements.Any(e => e.Id == id);
        }
    }
}
