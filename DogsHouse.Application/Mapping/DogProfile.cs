namespace DogsHouse.Application.Mapping;
using AutoMapper;
using DogsHouse.Application.Dto;
using DogsHouse.Domain.Entities;

public class DogProfile : Profile
{
    public DogProfile()
    {
        CreateMap<Dog, DogDto>();
        CreateMap<CreateDogDto, Dog>();
    }   
}