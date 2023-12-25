using Mapster;
using MapsterMapper;
using Radzen;
using System.Reflection;

namespace Prosthetics.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRadzenDependency(this IServiceCollection services)
        {
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<ContextMenuService>();
            services.AddScoped<TooltipService>();

            return services;
        }

        public static IServiceCollection AddMapster(this IServiceCollection services)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
            var mapperConfig = new Mapper(typeAdapterConfig);

            services.AddSingleton<IMapper>(mapperConfig);

            return services;
        }
    }
}
