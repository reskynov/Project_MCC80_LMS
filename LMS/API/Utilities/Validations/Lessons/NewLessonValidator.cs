using API.DTOs.Lessons;
using FluentValidation;

namespace API.Utilities.Validations.Lessons;

public class NewLessonValidator :AbstractValidator<NewLessonDto>
{
    public NewLessonValidator()
    {
        RuleFor(l => l.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(l => l.ClassroomGuid)
            .NotEmpty().WithMessage("Classrooom Guid is required");
    }
}
