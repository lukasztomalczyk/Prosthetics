using FileContextCore;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prosthetics.Common;
using Prosthetics.Extensions;
using Prosthetics.Features;
using Prosthetics.Persistance;
using Radzen;
using System.Reflection;

namespace Prosthetics
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Mapster
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            // scans the assembly and gets the IRegister, adding the registration to the TypeAdapterConfig
            typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
            // register the mapper as Singleton service for my application
            var mapperConfig = new Mapper(typeAdapterConfig);
            builder.Services.AddSingleton<IMapper>(mapperConfig);

            builder.Services.AddRadzenDependency();

            builder.Services.AddSingleton<IStore, Store>();
            builder.Services.AddSingleton<IDateTime, DateTimeService>();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddDbContext<ProstheticsDbContext>
                // (o => o.UseFileContextDatabase("MyDatabase"));
                (o => o.UseInMemoryDatabase("MyDatabase"));
            builder.Services.AddMediatR(typeof(MauiProgram).Assembly);
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
