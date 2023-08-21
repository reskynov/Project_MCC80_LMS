using API.Contracts;
using API.DTOs.Tasks;
using API.Repositories;
using FluentValidation;

namespace API.Utilities.Validations.Tasks;

public class TaskValidator : AbstractValidator<TaskDto>
{
    private readonly ITaskRepository _taskRepository;
    public TaskValidator(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;

        RuleFor(t => t.Attachment)
            .NotEmpty().WithMessage("Attachment is required");

        RuleFor(t => t.UserGuid)
            .NotEmpty().WithMessage("User guid is required")
            .Must((dto, userGuid) => IsDuplicateOrSame(userGuid, dto.LessonGuid))
            .WithMessage("A task with the same UserGuid and LessonGuid already exists.");

        RuleFor(t => t.LessonGuid)
            .NotEmpty().WithMessage("Lesson guid is required")
            .Must((dto, lessonGuid) => IsDuplicateOrSame(dto.UserGuid, lessonGuid))
            .WithMessage("A task with the same UserGuid and LessonGuid already exists.");
    }

    private bool IsDuplicateOrSame(Guid userGuid, Guid lessonGuid)
    {
        return _taskRepository.IsDataUnique(userGuid, lessonGuid);
    }
}
