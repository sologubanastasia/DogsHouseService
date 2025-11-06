using DogsHouse.Application.Dto;
using DogsHouse.Domain.Entities;
using DogsHouse.Infrastructure.Repositories;
using AutoMapper;

namespace DogsHouse.Application.Services;

public class DogService : IDogService
{
    private readonly IDogRepository _repository;
    private readonly IMapper _mapper;

    public DogService(IDogRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Dog>> GetDogsAsync(string? attribute, string? order, int? pageNumber, int? pageSize)
        => await _repository.GetDogsAsync(attribute, order, pageNumber, pageSize);

    public async Task AddDogAsync(CreateDogDto request)
    {
        if (request.TailLength < 0 || request.Weight < 0)
            throw new ArgumentException("Tail length and weight must be non-negative.");

        var existing = await _repository.GetByNameAsync(request.Name);
        if (existing != null)
            throw new ArgumentException("Dog with the same name already exists.");

        var dog = _mapper.Map<Dog>(request);

        await _repository.AddAsync(dog);
        await _repository.SaveChangesAsync();
    }
}
