using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kufar.Models
{
    public class AdvertisementDbContext : IdentityDbContext<User>
    {
        public AdvertisementDbContext(DbContextOptions<AdvertisementDbContext> options) 
            : base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<Advertisement> Advertisements { get; set; }
        
    }
}
