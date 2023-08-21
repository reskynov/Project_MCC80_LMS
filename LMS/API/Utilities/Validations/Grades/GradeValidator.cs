using API.Contracts;
using API.DTOs.Grades;
using FluentValidation;

namespace API.Utilities.Validations.Grades;

public class GradeValidator : AbstractValidator<GradeDto>
{
    private readonly IGradeRepository _gradeRepository;
    public GradeValidator(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;

        RuleFor(g => g.Guid)
            .NotEmpty().WithMessage("Guid is required");

        RuleFor(g => g.Value)
            .NotEmpty().WithMessage("Value is required. Please provide a valid integer value.");

        RuleFor(g => g.UserGuid)
            .NotEmpty().WithMessage("User guid is required")
            .Must((dto, userGuid) => IsDuplicateOrSame(userGuid, dto.TaskGuid))
            .WithMessage("A grade with the same UserGuid and TaskGuid already exists.");

        RuleFor(g => g.TaskGuid)
            .NotEmpty().WithMessage("Task guid is required")
            .Must((dto, taskGuid) => IsDuplicateOrSame(dto.UserGuid, taskGuid))
            .WithMessage("A grade with the same UserGuid and TaskGuid already exists.");
    }

    private bool IsDuplicateOrSame(Guid userGuid, Guid taskGuid)
    {
        return _gradeRepository.IsDataUnique(userGuid, taskGuid);
    }
}
