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

            modelBuilder.Entity<Country>()
                .HasMany(t => t.Cities)
                .WithOne(t => t.Country)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<City>()
            //    .HasOne(p => p.Country)
            //    .WithMany(t => t.Cities)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Advertisement>()
                .HasOne(p => p.Country)
                .WithMany(t => t.Advertisements)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Advertisement>()
                .HasOne(p => p.City)
                .WithMany(t => t.Advertisements)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
