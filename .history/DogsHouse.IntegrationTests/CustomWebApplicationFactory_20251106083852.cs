using DogsHouse.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DogsHouse.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            configBuilder.AddJsonFile("appsettings.Development.json", optional: false);
        });

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DogsDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var sp = services.BuildServiceProvider();
            var config = sp.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("DogsDb");

            services.AddDbContext<DogsDbContext>(options =>
                options.UseNpgsql(connectionString));

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DogsDbContext>();
            db.Database.EnsureCreated();
        });
    }
}
