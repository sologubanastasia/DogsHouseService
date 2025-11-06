using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using DogsHouse.Application.Services;
using DogsHouse.Application.Mapping;
namespace DogsHouse.Application;

public static class DependencyInjection
{
   public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDogService, DogService>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(typeof(DogProfile).Assembly);

        return services;
    }
}