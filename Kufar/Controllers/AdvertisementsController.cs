using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kufar.Models;
using Kufar.Services;
using Kufar.ViewModels;
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

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, SortType sortOrder = SortType.TitleAsc)
        {
            
            //var count = _advertisementsService.GetTotalCount();
            //var result = _advertisementsService.GetAdvertisements(pageNumber, pageSize, sortType);
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

                PageViewModel = new PageViewModel(count, pageNumber, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                Advertisements = items
            };


            return View(viewModel);
        }

  

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Photo, CityId, CountryId")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertisement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            return View(advertisement);
        }

        public JsonResult GetStateById(int id)
        {
           
            _list = _context.Cities.Where(a => a.Country.Id == id).ToList();
            _list.Insert(0, new City  { Id = 0, Name = "Select City"});
            return Json(new SelectList(_list, "Id", "Name"));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements.SingleOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }
            return View(advertisement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Photo, CityId, CountryId")] Advertisement advertisement)
        {
            if (id != advertisement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.Id))
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
            return View(advertisement);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisements.SingleOrDefaultAsync(m => m.Id == id);
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementExists(int id)
        {
            return _context.Advertisements.Any(e => e.Id == id);
        }
    }
}
