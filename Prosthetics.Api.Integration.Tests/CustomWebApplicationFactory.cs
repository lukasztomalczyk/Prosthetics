using System.Data.Common;
using JuniorDevOps.Net.Common.Mappers.Extensions;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Api.Persistance;

namespace Prosthetics.Api.Integration.Tests
{ 
    public class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProstheticsDbContext>));

                services.Remove(dbDescriptor);

                services.AddDbContext<ProstheticsDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDB");
                });

                var mapsterDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IMapper));
                services.Remove(mapsterDescriptor);
                services.AddMapster(typeof(Program).Assembly);
            });

            builder.UseEnvironment("Development");
        }
    }
}
