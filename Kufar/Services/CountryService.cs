using Kufar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kufar.Services
{
    public class CountryService : ICountryService
    {
        private readonly AdvertisementDbContext _context;
        public CountryService(AdvertisementDbContext context)
        {
            _context = context;
        }


        public async Task DeleteCountryAsync(int Id)
        {
            var country = await _context.Countries.SingleOrDefaultAsync(m => m.Id == Id);
            var nullCountryIdAdnCityId = _context.Advertisements.Where(x => x.Country.Id == Id);
            foreach (var item in nullCountryIdAdnCityId)
            {
                item.Country = null;
                item.City = null;

            }

            _context.Advertisements.UpdateRange(nullCountryIdAdnCityId);
            var cities = _context.Cities.Where(m => m.Country.Id == Id);
            _context.Cities.RemoveRange(cities);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
        }

    }

    public class CountryService2 : ICountryService
    {
        private readonly AdvertisementDbContext _context;
        public CountryService2(AdvertisementDbContext context)
        {
            _context = context;
        }
        public async Task DeleteCountryAsync(int Id)
        {
            throw new NotImplementedException();
        }

    }
}
