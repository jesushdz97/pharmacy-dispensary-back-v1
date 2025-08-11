using PharmacyDispensaryV1.Application;
using PharmacyDispensaryV1.Application.Services;
using PharmacyDispensaryV1.Infrastructure;
using PharmacyDispensaryV1.Infrastructure.Abstraction;
using PharmacyDispensaryV1.Infrastructure.Abstraction.Imp;
using PharmacyDispensaryV1.Infrastructure.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);

builder.Services.AddDbContext<PharmacyContext>();

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
