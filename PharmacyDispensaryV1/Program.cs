using Microsoft.EntityFrameworkCore;
using PharmacyDispensaryV1.Application;
using PharmacyDispensaryV1.Application.Services;
using PharmacyDispensaryV1.Infrastructure;
using PharmacyDispensaryV1.Infrastructure.Abstraction;
using PharmacyDispensaryV1.Infrastructure.Database.Context;
using PharmacyDispensaryV1.Infrastructure.Database.Interceptors;
using PharmacyDispensaryV1.Infrastructure.Middleware;
using Serilog;


var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .Build();

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSingleton<TimeStampInterceptor>();
    builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection")));

    builder.Services.AddScoped<PharmacyRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IPharmacyService, PharmacyService>();

    builder.Services.AddTransient<HttpLoggingMiddleware>();

    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();
    builder.Host.UseSerilog();

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

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
