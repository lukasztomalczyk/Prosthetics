using Microsoft.EntityFrameworkCore;

namespace Prosthetics.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void SeedEfDatabase<TDbContext>(this WebApplication app)
            where TDbContext : DbContext
        {
            using (var scope = app.Services.CreateScope())
            {
                var container = scope.ServiceProvider;
                var db = container.GetRequiredService<TDbContext>();
 
                try
                {
                    db.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = container.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
                }
            }
        }
    }
}
