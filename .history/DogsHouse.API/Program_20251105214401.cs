using DogsHouse.Application.Services;
using DogsHouse.Infrastructure;
using DogsHouse.Infrastructure.Repositories;
using DogsHouse.Api.Middleware;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
using DogsHouse.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddDbContext<DogsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DogsDb")));


builder.Services.AddRateLimiter(opt =>
{
    opt.AddFixedWindowLimiter("Fixed", o =>
    {
        o.PermitLimit = 10;
        o.Window = TimeSpan.FromSeconds(1);
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseRateLimiter();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();


public partial class Program { }