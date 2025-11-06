namespace DogsHouse.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection;
using DogsHouse.Infrastructure.Repositories;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DogsDbContext>(opt =>
          opt.UseNpgsql(configuration.GetConnectionString("DogsDb")));
            
        services.AddScoped<IDogRepository, DogRepository>();

        return services;
    }
}
