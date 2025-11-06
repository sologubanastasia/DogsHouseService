using DogsHouse.Domain.Entities;  
using DogsHouse.Infrastructure;
using Microsoft.EntityFrameworkCore;
using DogsHouse.Domain;
namespace DogsHouse.Infrastructure.Repositories;

public class DogRepository : IDogRepository
{
    private readonly DogsDbContext _context;
    public DogRepository(DogsDbContext context) => _context = context;

    public async Task<IEnumerable<Dog>> GetDogsAsync(string? attribute, string? order, int? pageNumber, int? pageSize)
    {
        IQueryable<Dog> query = _context.Dogs.AsQueryable();

        if (!string.IsNullOrEmpty(attribute))
        {
            query = attribute.ToLower() switch
            {
                "name" => order == "desc" ? query.OrderByDescending(d => d.Name) : query.OrderBy(d => d.Name),
                "color" => order == "desc" ? query.OrderByDescending(d => d.Color) : query.OrderBy(d => d.Color),
                "tail_length" => order == "desc" ? query.OrderByDescending(d => d.TailLength) : query.OrderBy(d => d.TailLength),
                "weight" => order == "desc" ? query.OrderByDescending(d => d.Weight) : query.OrderBy(d => d.Weight),
                _ => query
            };
        }

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        return await query.ToListAsync();
    }

    public Task<Dog?> GetByNameAsync(string name) =>
        _context.Dogs.FirstOrDefaultAsync(d => d.Name == name);

    public async Task AddAsync(Dog dog) => await _context.Dogs.AddAsync(dog);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}

