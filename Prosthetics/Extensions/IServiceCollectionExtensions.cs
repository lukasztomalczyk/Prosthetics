using Radzen;

namespace Prosthetics.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRadzenDependency(this IServiceCollection services)
        {
            services.AddScoped<DialogService>();

            return services;
        }
    }
}
