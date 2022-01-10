using FoodReviewAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReviewAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .HasIndex(e => e.Name);

            modelBuilder.Entity<MenuItem>()
                .HasIndex(e => e.Name);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsRequired();

            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(e => e.Name)
                .IsRequired();

            modelBuilder.Entity<Restaurant>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.Name)
                .IsRequired();

            modelBuilder.Entity<Review>()
                .Property(e => e.Rating)
                .IsRequired();
        }
    }
}
