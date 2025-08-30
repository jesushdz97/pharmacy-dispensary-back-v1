using Microsoft.EntityFrameworkCore;
using PharmacyDispensaryV1.Application;
using PharmacyDispensaryV1.Application.Services;
using PharmacyDispensaryV1.Infrastructure;
using PharmacyDispensaryV1.Infrastructure.Database.Context;
using PharmacyDispensaryV1.Infrastructure.Middleware;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);

builder.Services.AddDbContext<SqlDbContext>();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddTransient<HttpLoggingMiddleware>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<HttpLoggingMiddleware>();
//app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
