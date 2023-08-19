using API.DTOs.Lessons;
using FluentValidation;

namespace API.Utilities.Validations.Lessons;

public class LessonValidator : AbstractValidator<LessonDto>
{
    public LessonValidator()
    {
        RuleFor(l => l.Guid)
            .NotEmpty().WithMessage("Guid is required");

        RuleFor(l => l.Name)
            .NotEmpty().WithMessage("Name is required");
        
        RuleFor(l => l.ClassroomGuid)
            .NotEmpty().WithMessage("Classrooom Guid is required");
    }
}
