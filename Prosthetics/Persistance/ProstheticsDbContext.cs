using FileContextCore;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Models;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Persistance
{
    public class ProstheticsDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<AdditionalWork> AdditionalWorks { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public ProstheticsDbContext(DbContextOptions<ProstheticsDbContext> options) 
            : base(options)
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseFileContextDatabase(location: @".\database\");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(_ => _.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Order>()
                .HasMany(_ => _.AdditionalWorks)
                .WithMany(_ => _.Orders)
                .UsingEntity(_ => _.HasData(
                    new AdditionalWorkOrder()
                    {
                        Id = 1,
                        AdditionalWorksId = 1,
                        OrdersId = 1,
                    },
                    new AdditionalWorkOrder()
                    {
                        Id = 2,
                        AdditionalWorksId = 2,
                        OrdersId = 1,
                    }
                    ));

            modelBuilder.Entity<Patient>()
                .HasData(
                new Patient()
                { 
                    Id = 1,
                    FirstName = "Krzysiek",
                    LastName = "Kasprowicz"
                });

            modelBuilder.Entity<OrderType>()
                .HasData(
                new OrderType()
                {
                    Id = 1,
                    Name = "Aparat blokowy"
                },
                new OrderType()
                {
                    Id = 2,
                    Name = "Aparat Herbst'a"
                },
                new OrderType()
                {
                    Id = 3,
                    Name = "Aparat Hyrax"
                }
                );

            modelBuilder.Entity<AdditionalWork>().HasData(
                new AdditionalWork()
                {
                    Id = 1,
                    Name = "Akrylowe powierzchnie nagryzowe"
                },
                new AdditionalWork()
                {
                    Id = 2,
                    Name = "Dodatkowa śruba"
                },
                new AdditionalWork()
                {
                    Id = 3,
                    Name = "Dodatkowe elementy druciane"
                });

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
                    PatientId = 1,
                    OrderTypeId = 1,
                    Status = OrderStatus.New,
                    InsertedDate = DateTime.Now,
                    DeadLine = DateTime.Now.AddDays(7),
                    Comments = "To jest pierwszy komentarz, nie wiem jak długi"
                },
                new Order()
                {
                    Id = 2,
                    DoctorId = 1,
                    PatientId = 1,
                    OrderTypeId = 2,
                    Status = OrderStatus.Canceled,
                    InsertedDate = DateTime.Now.AddDays(1),
                    DeadLine = DateTime.Now.AddDays(8)
                },
                new Order()
                {
                    Id = 3,
                    DoctorId = 1,
                    PatientId = 1,
                    OrderTypeId = 3,
                    Status = OrderStatus.InProgress,
                    InsertedDate = DateTime.Now.AddDays(2),
                    DeadLine = DateTime.Now.AddDays(9)
                },
                new Order()
                {
                    Id = 4,
                    DoctorId = 1,
                    PatientId = 1,
                    OrderTypeId = 1,
                    Status = OrderStatus.Sent,
                    InsertedDate = DateTime.Now.AddDays(2),
                    DeadLine = DateTime.Now.AddDays(9)
                });
        }
    }
}
