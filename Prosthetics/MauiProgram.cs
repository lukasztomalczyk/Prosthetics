using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using JuniorDevOps.Net.SqlLite;
using JuniorDevOps.Net.SqlLite.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Prosthetics.Common;
using Prosthetics.Extensions;
using Prosthetics.Features;
using Prosthetics.Features.Doctors;
using Prosthetics.Persistance.Entities;
using ServiceStack.Data;
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
            builder.Services.AddSingleton<INotificationService, CommonNotificationService>();

            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
            builder.Services.AddSingleton<IStore, Store>();
            builder.Services.AddSingleton<IDateTime, DateTimeService>();
            builder.Services.AddSingleton<IExcelExporter, ExcelExporter>();
            builder.Services.AddMauiBlazorWebView();

            // SQLLITle
            var connectionString = $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Localdb.db")}";
            builder.Services.AddJuniorSqlLite(connectionString);

            builder.Services.AddScoped<IRequestHandler<GetDoctorsQuery, IEnumerable<DoctorDto>>>(_ =>
            {
                var connection = _.GetRequiredService<IDbConnectionFactory>();

                try
                {
                var repo = new SqlLiteRepository<Doctor>(connection);
                return new GetDoctorsQueryHandler(repo);

                }
                catch (Exception ex)
                {

                    throw;
                }
            });
            
            //builder.Services.AddDbContext<ProstheticsDbContext>
            //    // (o => o.UseFileContextDatabase("MyDatabase"));
            //    // (o => o.UseInMemoryDatabase("MyDatabase"));
            //    (o =>
            //    {

            //        // o.UseSqlite("Filename=" + Path.Combine(FileSystem.Current.CacheDirectory, $"LocalDatabase-{AppInfo.Current.BuildString}1.db"));
            //        o.UseSqlite("Filename=" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"LocalDatabase-{AppInfo.Current.BuildString}1.db"));
            //        o.EnableSensitiveDataLogging(true);

            //    });
           // builder.Services.AddScoped<ISqlLiteRepository<Doctor>,SqlLiteRepository<Doctor>>();

            //builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            builder.Services.AddMediatR(typeof(MauiProgram).Assembly);
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            //builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
