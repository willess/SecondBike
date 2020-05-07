using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecondBike.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondBike.Data
{
    public class SecondBikeContext : IdentityDbContext<User>
    {
        public SecondBikeContext(DbContextOptions<SecondBikeContext> options) : base(options)
        {
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MainCategory>()
                .HasMany(c => c.Categories)
                .WithOne(c => c.MainCategory);

            modelBuilder.Entity<Category>()
                .HasMany(a => a.Advertisements)
                .WithOne(c => c.Category);
               
        }
    }

}
