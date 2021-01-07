using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AccessMaiiaConexa.Models.Local
{
    public partial class LocalDataContext : DbContext
    {
        public LocalDataContext() : base()
        {
            Console.WriteLine("Created Context");
            ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
            ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
        }

        public LocalDataContext(DbContextOptions<LocalDataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin@2021",
                    Role = "manager"
                }
            );

            builder.Entity<User>().HasData(
                new User
                {
                    Id = 2,
                    Username = "user",
                    Password = "user@2021",
                    Role = "employee"
                }
            );

            builder.Entity<User>().HasData(
                new User
                {
                    Id = 3,
                    Username = "alexkads@gmail.com",
                    Password = "@Zenitp770128",
                    Role = "manager"
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.EnableSensitiveDataLogging(true);
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
