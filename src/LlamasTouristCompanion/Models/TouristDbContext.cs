using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace LlamasTouristCompanion.Models
{
    public class TouristDbContext : DbContext
    {
        public TouristDbContext(DbContextOptions<TouristDbContext> options) : base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
