using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kufar.Models
{
    public class AdvertisementDbContext : IdentityDbContext<User>
    {
        public AdvertisementDbContext(DbContextOptions<AdvertisementDbContext> options)
            : base(options)
        {

        }
        public DbSet<Advertisement> Advertisements { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
