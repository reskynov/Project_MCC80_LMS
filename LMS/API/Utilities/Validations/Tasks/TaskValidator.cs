using API.Contracts;
using API.DTOs.Tasks;
using API.Repositories;
using FluentValidation;

namespace API.Utilities.Validations.Tasks;

public class TaskValidator : AbstractValidator<TaskDto>
{
    public TaskValidator()
    {
        RuleFor(t => t.Attachment)
            .NotEmpty().WithMessage("Attachment is required");

        RuleFor(t => t.UserGuid)
            .NotEmpty().WithMessage("User guid is required");

        RuleFor(t => t.LessonGuid)
            .NotEmpty().WithMessage("Lesson guid is required");
    }
}
