using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Prosthetics.Api.Integration.Tests
{
    public abstract class WebApplicationBase<TProgram, TDbContext>
        where TProgram : class
        where TDbContext : DbContext
    {
        protected readonly CustomWebApplicationFactory<TProgram> _factory;
        protected readonly HttpClient _client;

        public WebApplicationBase(CustomWebApplicationFactory<TProgram> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        protected Task DbContextAction(Func<TDbContext, Task> dbContext)
        {
            var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<TDbContext>();

            return dbContext(db);
        }

        protected StringContent ConvertToBody(object value)
        {
            var serializedObject = JsonConvert.SerializeObject(value);

            return new StringContent(serializedObject, MediaTypeHeaderValue.Parse("application/json"));
        }
    }
}
