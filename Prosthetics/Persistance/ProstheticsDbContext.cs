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
        public DbSet<AdditionalWorkCount> AdditionalWorkCounts { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public ProstheticsDbContext(DbContextOptions<ProstheticsDbContext> options) 
            : base(options)
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO mozna wybrać lokaloizacje optionsBuilder.UseFileContextDatabase(location: @".\database\");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .Property(_ => _.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Order>()
                .HasMany(_ => _.AdditionalWorkCounts)
                .WithMany(_ => _.Orders);

            modelBuilder.Entity<AdditionalWork>()
                .HasMany(_ => _.AdditionalWorkCounts)
                .WithOne(_ => _.AdditionalWork);

            modelBuilder.Entity<AdditionalWorkCount>()
                .HasOne(_ => _.AdditionalWork)
                .WithMany(_ => _.AdditionalWorkCounts);

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


            //SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
               new Patient()
               {
                   Id = 1,
                   FirstName = "Krzysiek",
                   LastName = "Kasprowicz"
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
                    DeadLine = DateTime.Now.AddDays(9),
                    //AdditionalWorkCounts = new List<AdditionalWorkCount>() { new AdditionalWorkCount() { Id = 1, Count = 1, } }
                });
        }
    }
}
