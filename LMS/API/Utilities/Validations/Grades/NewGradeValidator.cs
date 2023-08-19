using API.Contracts;
using API.DTOs.Grades;
using FluentValidation;

namespace API.Utilities.Validations.Grades;

public class NewGradeValidator : AbstractValidator<NewGradeDto>
{
    private readonly IGradeRepository _gradeRepository;
    public NewGradeValidator(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;

        RuleFor(g => g.Value)
            .NotEmpty().WithMessage("Value is required. Please provide a valid integer value.")
            .InclusiveBetween(0, 100).WithMessage("Value must be between 0 and 100.");

        RuleFor(g => g.UserGuid)
            .NotEmpty().WithMessage("User guid is required")
            .Must((dto, userGuid) => IsDuplicateValue(userGuid, dto.TaskGuid))
            .WithMessage("A grade with the same UserGuid and TaskGuid already exists.");

        RuleFor(g => g.TaskGuid)
            .NotEmpty().WithMessage("Task guid is required")
            .Must((dto, taskGuid) => IsDuplicateValue(dto.UserGuid, taskGuid))
            .WithMessage("A grade with the same UserGuid and TaskGuid already exists.");
    }

    // Update IsDuplicateValue to take two string arguments
    private bool IsDuplicateValue(Guid userGuid, Guid taskGuid)
    {
        return _gradeRepository.IsNotExist(userGuid, taskGuid);
    }

}
