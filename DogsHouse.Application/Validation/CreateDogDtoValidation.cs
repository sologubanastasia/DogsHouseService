using FluentValidation;
using DogsHouse.Application.Dto;

namespace DogsHouse.Application.Validation;

public class CreateDogDtoValidator : AbstractValidator<CreateDogDto>
{
    public CreateDogDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Dog name is required.")
            .MaximumLength(100).WithMessage("Dog name is required.");

        RuleFor(c => c.Color)   
            .NotEmpty().WithMessage("Color is required");

        RuleFor(x => x.TailLength)
            .GreaterThanOrEqualTo(0).WithMessage("Tail length must be non-negative.");

        RuleFor(x => x.Weight)
            .GreaterThanOrEqualTo(0).WithMessage("Weight must be non-negative.");


    }
}