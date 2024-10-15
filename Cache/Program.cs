using Cache.Models;
using Cache.Servisler.Caching;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();   
});

builder.Host.UseSerilog((context, services, configuration) => configuration
    .WriteTo.Console() // Konsola loglama
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)); // Dosyaya loglamak için kullabnýlan kütüphane

builder.Services.AddDbContext<CacheContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddControllers().AddOData(opt => opt.EnableQueryFeatures());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
