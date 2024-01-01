using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using FileContextCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prosthetics.Common;
using Prosthetics.Extensions;
using Prosthetics.Features;
using Prosthetics.Persistance;

namespace Prosthetics
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMapster();
            builder.Services.AddRadzenDependency();
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IDialogService, WebDialogService>();
            builder.Services.AddSingleton<IJsonConverter, JsonConverter>();
            builder.Services.AddSingleton<IHttpRequestExecutor, HttpRequestExecutor>();

            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
            builder.Services.AddSingleton<IStore, Store>();
            builder.Services.AddSingleton<IDateTime, DateTimeService>();
            builder.Services.AddSingleton<IExcelExporter, ExcelExporter>();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddDbContext<ProstheticsDbContext>
                // (o => o.UseFileContextDatabase("MyDatabase"));
                // (o => o.UseInMemoryDatabase("MyDatabase"));
                (o =>
                {
                    o.UseSqlite("Filename=" + Path.Combine(FileSystem.Current.CacheDirectory, $"LocalDatabase-{AppInfo.Current.BuildString}1.db"));
                    o.EnableSensitiveDataLogging(true);
                });

        builder.Services.AddMediatR(typeof(MauiProgram).Assembly);
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
