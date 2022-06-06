using System.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Birlik.Data.Models;
using Birlik.Data.Context;

namespace Birlik.Data.Context
{
    public class BirlikAppContext : IdentityDbContext<BirlikUser>
    {
        public BirlikAppContext()
        {

        }

        public BirlikAppContext(DbContextOptions<BirlikAppContext> options)
            : base(options)
        {
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Location> Locations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Laptop connection string
            optionsBuilder.UseSqlServer("Server=tcp:localhost;Database=BirlikDb;User Id=hbuser;Password=hbuser1029");
            //PC connection string
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BirlikDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // UNIQUE additions
            builder.Entity<Country>().HasIndex(x=>x.CountryName).IsUnique(true);
            builder.Entity<City>().HasIndex(x=>x.CityName).IsUnique(true);
            builder.Entity<Location>().HasIndex(x=>x.CityId).IsUnique(true);
            
            //Relationship configuring 
            builder.Entity<Location>().HasOne(x=>x.City);
            builder.Entity<Location>().HasOne(x=>x.Country);
        }      
    }
}
