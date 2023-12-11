using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Persistance
{
    public class ProstheticsDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ProstheticsDbContext(DbContextOptions<ProstheticsDbContext> options) 
            : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor()
                {
                    Id = 1,
                    FirstName = "Łukasz",
                    LastName = "Kowalski"
                });

            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    Id = 1,
                    DoctorId = 1,
                    Type = "MD",
                    Status = 1,
                    InsertedDate = DateTime.Now,
                    DeadLine = DateTime.Now.AddDays(7)
                },
                new Order()
                {
                    Id = 2,
                    DoctorId = 1,
                    Type = "MD",
                    Status = 2,
                    InsertedDate = DateTime.Now.AddDays(1),
                    DeadLine = DateTime.Now.AddDays(8)
                },
                new Order()
                {
                    Id = 3,
                    DoctorId = 1,
                    Type = "MD",
                    Status = 3,
                    InsertedDate = DateTime.Now.AddDays(2),
                    DeadLine = DateTime.Now.AddDays(9)
                });
        }
    }
}
