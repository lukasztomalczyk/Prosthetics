using Prosthetics.Api.Persistance;
using Prosthetics.Api.Persistance.Entities;
using Xunit;
using Prosthetics.Features.Doctors;
using FluentAssertions;

namespace Prosthetics.Api.Integration.Tests.Features
{
    public class DoctorsEndpointsTests : WebApplicationBase<Program, ProstheticsDbContext>, IClassFixture<CustomWebApplicationFactory<Program>>
    {
        public DoctorsEndpointsTests(CustomWebApplicationFactory<Program> factor)
            : base (factor)
        {
        }

        [Fact]
        public async Task GetDoctorsQueryEndpoint()
        {
            await DbContextAction(async db =>
            {
                // Arrange
                var testData = new List<Doctor>()
                { 
                    new Doctor() { Id = 1, FirstName = "Krzysiek", LastName = "Kowalski" }
                };
                db.Doctors.AddRange(testData);
                db.SaveChanges();
                var expected = new List<DoctorDto>()
                { 
                    new DoctorDto() {  Id = 1, FullName = "Krzysiek Kowalski" }
                };
            
                // Act
                var response = await _client.GetAsync("doctors");

                // Assert
                response.CheckResponse();
                var result = await response.ConvertResponseAsync<IEnumerable<DoctorDto>>();
                result.Should().BeEquivalentTo(expected);
            });
        }

        [Fact]
        public async Task DeleteDoctorCommandEndpoint()
        {
            await DbContextAction(async db =>
            {
                // Arrange
                var testData = new List<Doctor>()
                {
                    new Doctor() { FirstName = "Krzysiek", LastName = "Kowalski" }
                };
                db.Doctors.AddRange(testData);
                db.SaveChanges();

                // Act
                var response = await _client.DeleteAsync("doctors?doctorId=2");

                // Assert
                response.CheckResponse();
                db.Doctors.FirstOrDefault(_ => _.FirstName == "Krzysiek" && _.LastName == "Kowalski").Should().NotBeNull();
            });
        }

        [Fact]
        public async Task AddDoctorCommand()
        {
            await DbContextAction(async db =>
            {
                // Act
                var response = await _client.PostAsync("doctors", ConvertToBody(new AddDoctorCommand() 
                { 
                    FirstName = "Lukasz", 
                    LastName = "Ciapa" 
                }));

                // Assert
                response.CheckResponse();
                db.Doctors.FirstOrDefault(_ => _.FirstName == "Lukasz" && _.LastName == "Ciapa").Should().NotBeNull();
            });
        }
    }
}
