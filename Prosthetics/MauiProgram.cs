using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using JuniorDevOps.Net.Common.Time;
using JuniorDevOps.Net.Http.Extensions;
using Microsoft.Extensions.Logging;
using Prosthetics.Common;
using Prosthetics.Extensions;
using Prosthetics.Features;
using System.Text.Json;
using JuniorDevOps.Net.Common.Mappers.Extensions;
using JuniorDevOps.Net.Common.Converters;

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
            builder.Services.AddJuniorHttpClient()
                .AddClient("backend", httpClient => 
                {
                    //httpClient.BaseAddress = new Uri("http://juniordevops.ddns.net:8080/");
                    httpClient.BaseAddress = new Uri("http://localhost:5174/");
                });
            builder.Services.AddSingleton<IDialogService, WebDialogService>();
            builder.Services.AddSingleton<IJsonConverter>(_ => new JsonConverter(new JsonSerializerOptions()));
            builder.Services.AddSingleton<IExcelExporter, ExcelExporter>();
            builder.Services.AddSingleton<INotificationService, CommonNotificationService>();

            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
            builder.Services.AddSingleton<IStore, Store>();
            builder.Services.AddSingleton<IDateTime, DateTimeService>();
  
            builder.Services.AddMauiBlazorWebView();
  
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
