using Carter;
using JuniorDevOps.Net.Common.Mappers.Extensions;
using JuniorDevOps.Net.Common.Time;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Api.Extensions;
using Prosthetics.Persistance;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.Listen(IPAddress.Parse("0.0.0.0"), 8080);
//});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMapster();
builder.Services.AddScoped<IDateTime, DateTimeService>();
builder.Services.AddCarter();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddDbContext<ProstheticsDbContext>(o =>
{
    o.UseSqlite("Filename=" + Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"LocalDatabase-1.db"));
    o.EnableSensitiveDataLogging(true);
});

var app = builder.Build();

app.SeedEfDatabase<ProstheticsDbContext>();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.MapCarter();
//app.UseHttpsRedirection();

app.Run();

public partial class Program { }