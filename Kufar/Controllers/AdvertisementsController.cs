using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kufar.Models;
using Kufar.Services;
using Kufar.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Kufar.Controllers
{
    [Authorize]
    public class AdvertisementsController : Controller
    {
        private readonly AdvertisementDbContext _context;

        private readonly IAdvertisementsService _advertisementsService;

        public AdvertisementsController(AdvertisementDbContext context, IAdvertisementsService advertisementsService)
        {
            _context = context;
            _advertisementsService = advertisementsService;
        }

        //[HttpPost]
        //public IActionResult Display(Int16 i)
        //{

        //    if (i == 1)
        //    {
        //        return View("Index");
        //    }
        //    return View(viewModel);
        //}
//        public async Task<IActionResult> Index_old(int pageNumber = 1, SortType sortOrder = SortType.TitleAsc)
//        {
//            int pageSize = 10;
//
//            IQueryable<Advertisement> source = _context.Advertisements;
//            
//            ViewData["TitleSort"] = sortOrder == SortType.TitleAsc ? SortType.TitleDesc : SortType.TitleAsc;
//            ViewData["DescriptionSort"] = sortOrder == SortType.DescriptionAsc ? SortType.DescriptionDesc : SortType.DescriptionAsc;
//
//            switch (sortOrder)
//            {
//                case SortType.TitleDesc:
//                    source = source.OrderByDescending(s => s.Title);
//                    break;
//                case SortType.TitleAsc:
//                    source = source.OrderBy(s => s.Title);
//                    break;
//                case SortType.DescriptionDesc:
//                    source = source.OrderByDescending(s => s.Description);
//                    break;
//                case SortType.DescriptionAsc:
//                    source = source.OrderBy(s => s.Description);
//                    break;
//                default:
//                    source = source.OrderBy(s => s.Title);
//                    break;
//            }
//
//            var count = await source.CountAsync();
//            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
//
//            PageViewModel pageViewModel = new PageViewModel(count, pageNumber, pageSize);
//            IndexViewModel viewModel = new IndexViewModel
//            {
//                PageViewModel = pageViewModel,
//                Advertisements = items,
//                SortViewModel = new SortViewModel(sortOrder)
//            };
//            return View(viewModel);
//        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, SortType sortType = SortType.Asc)
        {
            var count = _advertisementsService.GetTotalCount();

            //checpn

            var result = _advertisementsService.GetAdvertisements(pageNumber, pageSize, sortType);

            PageViewModel pageViewModel = new PageViewModel(count, pageNumber, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                SortViewModel = new SortViewModel() { SortColumn = "Title", SortType = sortType == SortType.Asc ? SortType.Desc : SortType.Asc },
                Advertisements = result
            };

            return View(viewModel);
        }

//        protected CheckPageNumber()
//        {
//            if (count / pageSize < pageNumber)
//            {
//                pageNumber = count / pageSize;
//            }
//        }

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Photo")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertisement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advertisement);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Photo")] Advertisement advertisement)
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
