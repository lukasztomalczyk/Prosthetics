using Prosthetics.Api.Persistance;
using Prosthetics.Api.Persistance.Entities;
using Xunit;
using Prosthetics.Features.Orders;
using FluentAssertions;
using Prosthetics.Features.AdditionalWorks;
using Prosthetics.Api.Models;

namespace Prosthetics.Api.Integration.Tests.Features
{
    public class OrdersEndpointsTests : WebApplicationBase<Program, ProstheticsDbContext>, IClassFixture<CustomWebApplicationFactory<Program>>
    {
        public OrdersEndpointsTests(CustomWebApplicationFactory<Program> factor)
            : base (factor) 
        { 
        }

        [Fact]
        public async Task AddOrderCommand()
        {
            await DbContextAction(async db =>
            {
                // Arrange
                var doctorId = db.Doctors.Add(new Doctor() { FirstName = "AddOrder", LastName = "Command" }).Entity.Id;
                var patientId = db.Patients.Add(new Patient() { FirstName = "PatientAddOrder", LastName = "Command" }).Entity.Id;
                var orderTypeId = db.OrderTypes.Add(new OrderType() { Name = "FirstOrderType" }).Entity.Id;
                var additionalWorkId = db.AdditionalWorks.Add(new AdditionalWork() { Name = "Dodatkowa prawa" }).Entity.Id;
                db.SaveChanges();

                // Act
                var response = await _client.PostAsync("orders", ConvertToBody(new AddOrderCommand()
                {
                    DoctorId = doctorId,
                    PatientId = patientId,
                    OrderTypeId = orderTypeId,
                    DeadLine = DateTime.Parse("2024-01-01"),
                    AdditionalWorks = new List<AdditionalWorkCountDto>()
                    {
                        new AdditionalWorkCountDto() { Id = additionalWorkId, Count = "22", Name = "Dodatkowa prawa" }
                    }
                }));

                // Assert
                response.CheckResponse();
                var orderId = await response.ConvertResponseAsync<int>();
                var existing = db.Orders.FirstOrDefault(_ => _.Id == orderId);
                existing.Should().NotBeNull();
            });
        }

        [Fact]
        public async Task ChangeOrderStatusEndpoint()
        {
            await DbContextAction(async db =>
            {
                // Arrange
                var orderId = db.Orders.Add(new Order()
                {
                    DeadLine = DateTime.Parse("2024-01-02"),
                    Status = (int)OrderStatus.New
                })
                .Entity.Id;
                db.SaveChanges();

                // Act
                var response = await _client.PutAsync("orders/change-status", ConvertToBody(new ChangeOrderStatusCommand()
                { 
                    OrderId = orderId, 
                    NewOrderStatusId = 2
                }));

                // Assert
                response.CheckResponse();
                db.Orders.FirstOrDefault(_ => _.Id == orderId && _.Status == OrderStatus.Canceled).Should().NotBeNull();
            });
        }
    }
}
