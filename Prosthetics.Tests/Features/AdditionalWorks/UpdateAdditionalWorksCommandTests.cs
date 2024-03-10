//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using Prosthetics.Features.AdditionalWorks;
//using Prosthetics.Persistance.Entities;

//namespace Prosthetics.Tests.Features.AdditionalWorks
//{
//    public class UpdateAdditionalWorksCommandTests
//    {
//        [Fact]
//        public async Task UpdateAdditionalWorksCommandHandler_ShouldUpdateAdditionalWorksList()
//        {
//            // Arrange
//            using var dbInMemory = ProstheticsDbContextHelper.Create();
//            var seed = new List<AdditionalWork>()
//            {
//                new AdditionalWork()
//                {
//                    Id = 1,
//                    Name = "Akrylowe powierzchnie nagryzowe"
//                },
//                new AdditionalWork()
//                {
//                    Id = 2,
//                    Name = "Dodatkowa śruba"
//                },
//                new AdditionalWork()
//                {
//                    Id = 3,
//                    Name = "Dodatkowe elementy druciane"
//                }
//            };
//            dbInMemory.AdditionalWorks.AddRange(seed);
//            var doctor = new Doctor()
//            {
//                Id = 1,
//                FirstName = "Łukasz",
//                LastName = "Kowalski"
//            };
//            dbInMemory.Doctors.Add(doctor);
//            var orderType = new OrderType()
//            {
//                Id = 1,
//                Name = "Aparat blokowy"
//            };
//            dbInMemory.OrderTypes.Add(orderType);
//            var patient = new Patient()
//            {
//                Id = 1,
//                FirstName = "Krzysiek",
//                LastName = "Kasprowicz"
//            };
//            dbInMemory.Patients.Add(patient);
//            await dbInMemory.SaveChangesAsync();
//            var order = new Order()
//            {
//                Id = 1,
//                PatientId = 1,
//                OrderTypeId = 1,
//                DoctorId = 1,
//                AdditionalWorkCounts = new List<AdditionalWorkCount>
//                {
//                    new AdditionalWorkCount()
//                    {
//                        Id = 1,
//                        Count = 1,
//                        AdditionalWorkId = 1
//                    },
//                    new AdditionalWorkCount()
//                    {
//                        Id = 2,
//                        Count = 2,
//                        AdditionalWorkId = 2
//                    }
//                }
//            };
//            dbInMemory.Orders.Add(order);

//            await dbInMemory.SaveChangesAsync();
//            dbInMemory.ChangeTracker.Clear();

//            var request = new UpdateAdditionalWorksCommand()
//            {
//                OrderId = 1,
//                AdditionalCountWorks = new List<EditedAdditionalCountWorkDto>()
//                { 
//                    new EditedAdditionalCountWorkDto()
//                    { 
//                        AdditionalWorkId = 1,
//                        Count = 1
//                    },
//                    new EditedAdditionalCountWorkDto()
//                    {
//                        AdditionalWorkId = 3,
//                        Count = 3,
//                    }

//                }
//            };

//            var expectedResult = new List<AdditionalWorkCount>()
//            {
//                new AdditionalWorkCount()
//                {
//                    Id = 1,
//                    Count = 1,
//                    AdditionalWorkId = 1,
//                    //AdditionalWork = new AdditionalWork()
//                    //{
//                    //    Id = 1,
//                    //    Name = "Akrylowe powierzchnie nagryzowe"
//                    //},
//                },
//                new AdditionalWorkCount()
//                {
//                    Id = 3,
//                    Count = 3,
//                    AdditionalWorkId = 3,
//                    //AdditionalWork = new AdditionalWork()
//                    //{
//                    //    Id = 3,
//                    //    Name = "Dodatkowe elementy druciane"
//                    //}
//                }
//            };
//            var handler = new UpdateAdditionalWorksCommandHandler(dbInMemory);

//            // Act
//            await handler.Handle(request, new CancellationToken());

//            // Assert
//            var result = await dbInMemory.AdditionalWorkCounts.Where(_ => _.Orders.Any(x => x.Id == 1))
//                .Select(_ => new AdditionalWorkCount()
//                { 
//                    Id = _.Id, Count = _.Count, AdditionalWorkId = _.AdditionalWorkId
//                })
//                .ToListAsync();
//            result.Should().BeEquivalentTo(expectedResult);

//        }
//    }
//}
