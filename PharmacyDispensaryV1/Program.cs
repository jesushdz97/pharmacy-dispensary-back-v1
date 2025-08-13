using Microsoft.EntityFrameworkCore;
using PharmacyDispensaryV1.Application;
using PharmacyDispensaryV1.Application.Services;
using PharmacyDispensaryV1.Infrastructure;
using PharmacyDispensaryV1.Infrastructure.Abstraction;
using PharmacyDispensaryV1.Infrastructure.Database.Context;
using PharmacyDispensaryV1.Infrastructure.Database.Interceptors;
using Serilog;
using PharmacyDispensaryV1.Controllers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConection");

builder
    .Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);

builder.Services.AddSingleton<TimeStampInterceptor>();
builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<PharmacyRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPharmacyService, PharmacyService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
