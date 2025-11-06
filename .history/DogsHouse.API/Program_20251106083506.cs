=using DogsHouse.Application.Services;
using DogsHouse.Infrastructure;
using DogsHouse.Infrastructure.Repositories;
using DogsHouse.Api.Middleware;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
using DogsHouse.Application;

var builder = WebApplication.CreateBuilder(args);

// Додавання базових конфігурацій для різних середовищ (наприклад, Development, Production тощо)
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Додаємо FluentValidation для автоматичної валідації
builder.Services.AddFluentValidationAutoValidation();

// Додаємо додаток та інфраструктуру
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Додаємо підтримку контролерів
builder.Services.AddControllers();

// Підключення до БД за допомогою рядка з'єднання з конфігурації
builder.Services.AddDbContext<DogsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DogsDb")));

// Налаштування RateLimiter
builder.Services.AddRateLimiter(opt =>
{
    opt.AddFixedWindowLimiter("Fixed", o =>
    {
        o.PermitLimit = 10;
        o.Window = TimeSpan.FromSeconds(1);
    });
});

// Налаштування для OpenAPI (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Використовуємо middleware для обробки помилок
app.UseMiddleware<ExceptionMiddleware>();

// Налаштовуємо використання RateLimiter
app.UseRateLimiter();

// Підключення контролерів
app.MapControllers();

// Підключення Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

public partial class Program { }
