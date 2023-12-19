using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Persistance
{
    public class ProstheticsDbContext : DbContext
    {
        public DbSet<DoctorEntity> Doctors { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }

        public ProstheticsDbContext(DbContextOptions<ProstheticsDbContext> options) 
            : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderEntity>()
                .HasMany(_ => _.AdditionalWorks)
                .WithMany(_ => _.Orders);

            modelBuilder.Entity<AdditionalWorkEntityOrderEntity>();

            modelBuilder.Entity<AdditionalWorkEntityOrderEntity>().HasData(
                new AdditionalWorkEntityOrderEntity()
                { 
                    Id = 1,
                    AdditionalWorkId = 1,
                    OrderId = 1,
                },
                new AdditionalWorkEntityOrderEntity()
                {
                    Id = 2,
                    AdditionalWorkId = 2,
                    OrderId = 1,
                });

            modelBuilder.Entity<AdditionalWorkEntity>().HasData(
                new AdditionalWorkEntity()
                {
                    Id = 1,
                    Name = "Akrylowe powierzchnie nagryzowe"
                },
                new AdditionalWorkEntity()
                {
                    Id = 2,
                    Name = "Dodatkowa śruba"
                },
                new AdditionalWorkEntity()
                {
                    Id = 3,
                    Name = "Dodatkowe elementy druciane"
                });

            modelBuilder.Entity<DoctorEntity>().HasData(
                new DoctorEntity()
                {
                    Id = 1,
                    FirstName = "Łukasz",
                    LastName = "Kowalski"
                });

            modelBuilder.Entity<OrderEntity>().HasData(
                new OrderEntity()
                {
                    Id = 1,
                    DoctorId = 1,
                    Type = "MD",
                    Status = 1,
                    InsertedDate = DateTime.Now,
                    DeadLine = DateTime.Now.AddDays(7),
                    Comments = "To jest pierwszy komentarz, nie wiem jak długi"
                },
                new OrderEntity()
                {
                    Id = 2,
                    DoctorId = 1,
                    Type = "MD",
                    Status = 2,
                    InsertedDate = DateTime.Now.AddDays(1),
                    DeadLine = DateTime.Now.AddDays(8)
                },
                new OrderEntity()
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
