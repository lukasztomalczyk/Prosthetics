using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Features.Admin;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Tests.Features.Admin
{
    public class GetOrdersByDateRangeQueryTests
    {
        [Fact]
        public async Task GetOrdersByDateRangeQueryHandler_ShouldReturnOrderWithCorrectStructure()
        {
            // Arrange
            using var dbInMemory = ProstheticsDbContextHelper.Create();
            var orderTypes = new List<OrderType>()
            {
                new OrderType()
                {
                    Id = 1,
                    Name = "Główny typ 1"
                },
                new OrderType()
                {
                    Id = 2,
                    Name = "Główny typ 2"
                }
            };
            var additionalWorks = new List<AdditionalWork>()
            {
                new AdditionalWork()
                {
                    Id = 1,
                    Name = "Dodatkowe zlecenie 1"
                },
                new AdditionalWork()
                {
                    Id = 2,
                    Name = "Dodatkowe zlecenie 2"
                },
                new AdditionalWork()
                {
                    Id = 3,
                    Name = "Dodatkowe zlecenie 3"
                }
            };
            var patients = new List<Patient>()
            {
                new Patient()
                {
                    Id = 1,
                    FirstName = "Krzysztof",
                    LastName = "Stanowski"
                },
                new Patient()
                {
                    Id = 2,
                    FirstName = "Damian",
                    LastName = "Duda"
                },
                new Patient() 
                {
                    Id = 3,
                    FirstName = "Rafał",
                    LastName = "Jarosz"
                }
            };
            dbInMemory.Patients.AddRange(patients);
            dbInMemory.OrderTypes.AddRange(orderTypes);
            dbInMemory.AdditionalWorks.AddRange(additionalWorks);
            await dbInMemory.SaveChangesAsync();

            var doctors = new List<Doctor>()
            {
                new Doctor()
                {
                    Id = 1,
                    FirstName = "Łukasz",
                    LastName = "Kowalczyk",
                    Orders = new List<Order>()
                    {
                        new Order()
                        {
                            Id = 1,
                            PatientId = patients[0].Id,
                            OrderTypeId = orderTypes[0].Id,
                            AdditionalWorkCounts = new List<AdditionalWorkCount>()
                            {
                                new AdditionalWorkCount()
                                {
                                    Id = 5,
                                    Count = 1,
                                    AdditionalWorkId = 1,
                                },
                                new AdditionalWorkCount()
                                {
                                    Id = 6,
                                    Count = 1,
                                    AdditionalWorkId = 2,
                                },
                                new AdditionalWorkCount()
                                {
                                    Id = 7,
                                    Count = 1,
                                    AdditionalWorkId = 3,
                                },
                            },
                            InsertedDate = DateTime.Now,
                        },
                        new Order()
                        {
                            Id = 2,
                            PatientId = patients[0].Id,
                            OrderTypeId = orderTypes[1].Id,
                            AdditionalWorkCounts = new List<AdditionalWorkCount>()
                            {
                                new AdditionalWorkCount()
                                {
                                    Id = 3,
                                    Count = 1,
                                    AdditionalWorkId = 2,
                                },
                                new AdditionalWorkCount()
                                {
                                    Id = 4,
                                    Count = 1,
                                    AdditionalWorkId = 3,
                                },
                            },
                            //dbInMemory.AdditionalWorks.Where(_ => _.Id != 1).ToList(),
                            InsertedDate = DateTime.Now,
                        }
                    }
                },
                new Doctor()
                {
                    Id = 2,
                    FirstName = "Damian",
                    LastName = "Kowalski",
                    Orders = new List<Order>()
                    {
                        new Order()
                        {
                            Id = 3,
                            PatientId = patients[1].Id,
                            OrderTypeId = orderTypes[1].Id,
                             //dbInMemory.AdditionalWorks.Where(_ => _.Id != 2).ToList(),
                            AdditionalWorkCounts = new List<AdditionalWorkCount>()
                            {
                                new AdditionalWorkCount()
                                {
                                    Id = 1,
                                    Count = 1,
                                    AdditionalWorkId = 1,
                                },
                                new AdditionalWorkCount()
                                {
                                    Id = 2,
                                    Count = 1,
                                    AdditionalWorkId = 3,
                                },
                            },
                            InsertedDate = DateTime.Now,
                        },
                        new Order() 
                        {
                            Id = 4,
                            PatientId = patients[2].Id,
                            OrderTypeId = orderTypes[1].Id,
                            InsertedDate = DateTime.Now,
                        }
                    }
                }
            };
            dbInMemory.Doctors.AddRange(doctors);
          //  dbInMemory.Doctors.Add(doctors[0]);
            await dbInMemory.SaveChangesAsync();
            var handler = new GetDoctorsOrdersByPatientQueryHandler(dbInMemory);

            // Act
            var result = await handler.Handle(new GetDoctorsOrdersByPatientQuery() { From = DateTime.Now.AddDays(-1), To = DateTime.Now.AddDays(1) }, new CancellationToken());

            // Assert
            var expectedResult = new List<DoctorOrdersByPatientDto>()
            {
                new DoctorOrdersByPatientDto()
                {
                    DoctorFullName = "Kowalski Damian",
                    OrdersByPatients = new List<ParientOrdersDto>()
                    {
                        new ParientOrdersDto()
                        {
                            PatientFullName = "Jarosz Rafał",
                            Orders = new List<OrderCountDto>()
                            {
                                new OrderCountDto()
                                {
                                    OrderName = "Główny typ 2",
                                    Count = 1
                                }
                            },
                        },
                        new ParientOrdersDto()
                        {
                            PatientFullName = "Duda Damian",
                            Orders = new List<OrderCountDto>()
                            {
                                new OrderCountDto()
                                {
                                    OrderName = "Główny typ 2",
                                    Count = 1
                                },
                                new OrderCountDto()
                                {
                                    OrderName = "Dodatkowe zlecenie 1",
                                    Count = 1
                                },
                                new OrderCountDto()
                                {
                                    OrderName = "Dodatkowe zlecenie 3",
                                    Count = 1
                                }
                            }
                        },
                    }
                },
                new DoctorOrdersByPatientDto()
                {
                    DoctorFullName = "Kowalczyk Łukasz",
                    OrdersByPatients = new List<ParientOrdersDto>()
                    {
                        new ParientOrdersDto()
                        {
                            PatientFullName = "Stanowski Krzysztof",
                            Orders = new List<OrderCountDto>()
                            {
                                new OrderCountDto()
                                {
                                    OrderName = "Główny typ 1",
                                    Count = 1
                                },
                                new OrderCountDto()
                                {
                                    OrderName = "Dodatkowe zlecenie 1",
                                    Count = 1
                                },
                                new OrderCountDto()
                                {
                                    OrderName = "Główny typ 2",
                                    Count = 1
                                },
                                new OrderCountDto()
                                {
                                    OrderName = "Dodatkowe zlecenie 3",
                                    Count = 2
                                },
                                new OrderCountDto()
                                {
                                    OrderName = "Dodatkowe zlecenie 2",
                                    Count = 2
                                },
                            }
                        }
                    }
                },
            };
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
