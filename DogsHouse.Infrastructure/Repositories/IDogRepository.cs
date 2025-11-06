using DogsHouse.Domain.Entities;
using DogsHouse.Domain;
namespace DogsHouse.Infrastructure.Repositories;

public interface IDogRepository
{
    Task<IEnumerable<Dog>> GetDogsAsync(string? sortAttribute, string? order, int? pageNumber, int? pageSize);
    Task<Dog?> GetByNameAsync(string name);
    Task AddAsync(Dog dog);
    Task SaveChangesAsync();
}
