﻿using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MealsOrderingApplication.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        // Add Db Sets

        // Users
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        // Food
        public DbSet<Category> Categories { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Drink> Drinks { get; set; }

        // Orders
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }

        // Reviews
        public DbSet<Review> Reviews { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Config Identity Tables Names
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");

            // Apply Entities Configurations
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
