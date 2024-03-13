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
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Microsoft.Extensions.DependencyInjection;

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
                    /// https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/local-web-services?view=net-maui-8.0#local-web-services-running-over-http
                   // httpClient.BaseAddress = new Uri("http://juniordevops.ddns.net:8080/");
                    //httpClient.BaseAddress = new Uri("http://192.168.1.47:5173/");
                    httpClient.BaseAddress = new Uri("https://10.0.2.2:7275/");
                })
                .ConfigurePrimaryHttpMessageHandler(_ => 
                {
                    HttpClientHandler handler = new HttpClientHandler();
                    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                    {
                        if (cert.Issuer.Equals("CN=localhost"))
                            return true;
                        return errors == System.Net.Security.SslPolicyErrors.None;
                    };

                    return handler;
                });
              
            builder.Services.AddSingleton<IDialogService, WebDialogService>();

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
        
        private static Func<HttpRequestMessage, X509Certificate2?, X509Chain?, SslPolicyErrors, bool> BayPassSslValidation  = (message, cert, chain, errors) =>
        {
            if (cert.Issuer.Equals("CN=localhost"))
                return true;
            return errors == System.Net.Security.SslPolicyErrors.None;
        };
}
}
