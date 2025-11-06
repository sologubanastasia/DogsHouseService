using Xunit;
using Moq;
using AutoMapper;
using FluentAssertions;
using DogsHouse.Application.Dto;
using DogsHouse.Application.Services;
using DogsHouse.Domain.Entities;
using DogsHouse.Infrastructure.Repositories;

public class DogServiceTests
{
    private readonly Mock<IDogRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly DogService _service;

    public DogServiceTests()
    {
        _repositoryMock = new Mock<IDogRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new DogService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AddDogAsync_Should_Add_Dog_When_Valid()
    {
        var dto = new CreateDogDto { Name = "Rex", Color = "Brown", TailLength = 10, Weight = 20 };
        _repositoryMock.Setup(r => r.GetByNameAsync(dto.Name)).ReturnsAsync((Dog?)null);
        _mapperMock.Setup(m => m.Map<Dog>(dto)).Returns(new Dog { Name = dto.Name });

        await _service.AddDogAsync(dto);

        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Dog>()), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task AddDogAsync_Should_Throw_When_Negative_TailLength_Or_Weight()
    {
        var dto = new CreateDogDto { Name = "Rex", Color = "Brown", TailLength = -5, Weight = 10 };

        var act = async () => await _service.AddDogAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Tail length and weight must be non-negative.");
    }

    [Fact]
    public async Task AddDogAsync_Should_Throw_When_Dog_With_Same_Name_Exists()
    {
        var dto = new CreateDogDto { Name = "Buddy", Color = "Black", TailLength = 10, Weight = 15 };
        _repositoryMock.Setup(r => r.GetByNameAsync(dto.Name)).ReturnsAsync(new Dog { Name = dto.Name });

        var act = async () => await _service.AddDogAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Dog with the same name already exists.");
    }
}
