using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiRESTWithNet6.Models
{
    public partial class NetCoreWebApiContext : DbContext
    {
        public NetCoreWebApiContext()
        {
        }

        public NetCoreWebApiContext(DbContextOptions<NetCoreWebApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(Settings.GetDefaulConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
            });

            var adminUser = new User()
            {
                Id = -1,
                Name = "Admin",
                LastName = "Admin",
                Email = "admin@email.com",
                Role = "Admin",
                UserName = "admin",
                Password = Security.CreateSHA256("admin")
            };

            modelBuilder.Entity<User>().HasData(adminUser);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
