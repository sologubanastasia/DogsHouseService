using DogsHouse.Application.Dto;
using DogsHouse.Domain.Entities;

namespace DogsHouse.Application.Services;

public interface IDogService
{
    Task<IEnumerable<Dog>> GetDogsAsync(string? attribute, string? order, int? pageNumber, int? pageSize);
    Task AddDogAsync(CreateDogDto request);
}
