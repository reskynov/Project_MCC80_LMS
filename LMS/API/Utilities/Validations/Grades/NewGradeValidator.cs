using API.DTOs.Grades;
using FluentValidation;

namespace API.Utilities.Validations.Grades;

public class NewGradeValidator : AbstractValidator<NewGradeDto>
{
    public NewGradeValidator()
    {
        RuleFor(g => g.Value)
            .NotEmpty().WithMessage("Value is required. Please provide a valid integer value.")
            .InclusiveBetween(0, 100).WithMessage("Value must be between 0 and 100."); ;

        RuleFor(g => g.UserGuid)
            .NotEmpty().WithMessage("User guid is required");

        RuleFor(g => g.TaskGuid)
            .NotEmpty().WithMessage("Task guid is required");
    }
}
