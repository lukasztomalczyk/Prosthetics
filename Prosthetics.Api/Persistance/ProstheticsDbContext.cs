using Microsoft.EntityFrameworkCore;
using Prosthetics.Api.Models;
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
                    Name = "Dodatkowe elementy druciane"
                },
                new AdditionalWork()
                {
                    Id = 2,
                    Name = "Akrylowe powierzchnie nagryzowe"
                },
                new AdditionalWork()
                {
                    Id = 3,
                    Name = "Dodatkowa śruba"
                },
                new AdditionalWork()
                {
                    Id = 4,
                    Name = "Zapora dla języka"
                });

            modelBuilder.Entity<OrderType>()
                .HasData(
                new OrderType()
                {
                    Id = 1,
                    Name = "Model diagnostyczny"
                },
                new OrderType()
                {
                    Id = 2,
                    Name = "Aparat Schwarza"
                },
                new OrderType()
                {
                    Id = 3,
                    Name = "Szyna retencyjna"
                },
                new OrderType()
                {
                    Id = 4,
                    Name = "Aparat Marco Rosa"
                },
                new OrderType()
                {
                    Id = 5,
                    Name = "Aparat Hyrax"
                },
                new OrderType()
                {
                    Id = 6,
                    Name = "Aparat Schwarza ze śrubą Bertoniego"
                },
                new OrderType()
                {
                    Id = 7,
                    Name = "Twinblock"
                },
                new OrderType()
                {
                    Id = 8,
                    Name = "Szyna Michigan"
                },
                new OrderType()
                {
                    Id = 9,
                    Name = "Szyna nagryzowa"
                },
                new OrderType()
                {
                    Id = 10,
                    Name = "Dystalizator/Mezjalizator"
                },
                new OrderType()
                {
                    Id = 11,
                    Name = "Hybryd Hyrax 2mikro"
                },
                new OrderType()
                {
                    Id = 12,
                    Name = "Hybrid Hyrax 4mikro"
                },
                new OrderType()
                {
                    Id = 13,
                    Name = "Łuk językowy lutowany"
                },
                new OrderType()
                {
                    Id = 14,
                    Name = "Łuk podniebienny lutowany"
                },
                new OrderType()
                {
                    Id = 15,
                    Name = "Łuk podniebienny - wkładany"
                },
                new OrderType()
                {
                    Id = 16,
                    Name = "Naprawa"
                },
                new OrderType()
                {
                    Id = 17,
                    Name = "Pendex"
                },
                new OrderType()
                {
                    Id = 18,
                    Name = "Pendulum"
                },
                new OrderType()
                {
                    Id = 19,
                    Name = "Płytka Hawleya"
                },
                new OrderType()
                {
                    Id = 20,
                    Name = "Powielenie modelu"
                },
                new OrderType()
                {
                    Id = 21,
                    Name = "Quad Helix"
                },
                new OrderType()
                {
                    Id = 22,
                    Name = "Silensor"
                },
                new OrderType()
                {
                    Id = 23,
                    Name = "Szyna wybielająca"
                },
                new OrderType()
                {
                    Id = 24,
                    Name = "Utrzymywacz przestrzeni"
                },
                new OrderType()
                {
                    Id = 25,
                    Name = "Lip Bumper"
                },
                new OrderType()
                {
                    Id = 26,
                    Name = "Deprogramator Koisa"
                },
                new OrderType()
                {
                    Id = 27,
                    Name = "Aparat Retencyjny"
                },
                new OrderType()
                {
                    Id = 28,
                    Name = "Aparat Nance'a"
                },
                new OrderType()
                {
                    Id = 29,
                    Name = "Aparat Herbst'a"
                },
                new OrderType()
                {
                    Id = 30,
                    Name = "Aparat blokowy"
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
