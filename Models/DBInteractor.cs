using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CitiesLocation.Models
{
    public class DBInteractor : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=TestDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>().ToTable("users", "test");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<City>().ToTable("cities", "test");

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityId);
                entity.Property(e => e.Name);
                entity.Property(e => e.AsciiName);
                entity.Property(e => e.Latitude);
                entity.Property(e => e.Longitude);
                entity.Property(e => e.Country);
                entity.Property(e => e.Iso2);
                entity.Property(e => e.Iso3);
                entity.Property(e => e.AdminName);
                entity.Property(e => e.Capital);
                entity.Property(e => e.Population);               
            });

            base.OnModelCreating(modelBuilder);            

        }
    }
}
