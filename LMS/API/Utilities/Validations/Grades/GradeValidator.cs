using API.Contracts;
using API.DTOs.Grades;
using FluentValidation;

namespace API.Utilities.Validations.Grades;

public class GradeValidator : AbstractValidator<GradeDto>
{
    public GradeValidator()
    {
        RuleFor(g => g.Guid)
            .NotEmpty().WithMessage("Guid is required");

        RuleFor(g => g.Value)
            .NotEmpty().WithMessage("Value is required. Please provide a valid integer value.");

        RuleFor(g => g.UserGuid)
            .NotEmpty().WithMessage("User guid is required");

        RuleFor(g => g.TaskGuid)
            .NotEmpty().WithMessage("Task guid is required");
    }
}
