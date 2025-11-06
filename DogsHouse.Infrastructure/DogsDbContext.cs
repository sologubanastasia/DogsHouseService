namespace DogsHouse.Infrastructure;
using DogsHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class DogsDbContext : DbContext
{
    public DogsDbContext(DbContextOptions<DogsDbContext> options) :base(options) 
    {

    }

    public DbSet<Dog> Dogs => Set<Dog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Dog>().HasData(
            new Dog { Id = 1, Name = "Buddy", Color = "Brown", TailLength = 15, Weight = 20 },
            new Dog { Id = 2, Name = "Max", Color = "Black", TailLength = 10, Weight = 25 },
            new Dog { Id = 3, Name = "Bella", Color = "White", TailLength = 12, Weight = 18 }
        );
    }
}
