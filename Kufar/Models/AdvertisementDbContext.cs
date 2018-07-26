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

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        

        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // configures one-to-many relationship
            //modelBuilder.Entity<Advertisement>().HasOne(t => t.Country);

            //modelBuilder.Entity<Country>().HasMany(t => t.Cities);
        }
    }
}
