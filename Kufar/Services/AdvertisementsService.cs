using System.Collections.Generic;
using System.Linq;
using Kufar.Models;
using Microsoft.EntityFrameworkCore;

namespace Kufar.Services
{
    public class AdvertisementsService : IAdvertisementsService
    {
        private readonly AdvertisementDbContext _context;

        public AdvertisementsService(AdvertisementDbContext context)
        {
            _context = context;
        }

        public IList<Advertisement> GetAdvertisements(int pageNumber, int pageSize, SortType sortType)
        {
            var items = _context.Advertisements
                .Include(adv => adv.City)
                .Include(adv => adv.Country)
                .OrderBy(adv => adv.Title);


            //if (sortType == SortType.Desc)
            //{
            //    items = items.OrderByDescending(adv => adv.Title);
            //}
            



            var paged = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return paged.ToList();
        }

        public int GetTotalCount()
        {
            return _context.Advertisements.Count();
        }
    }
}
