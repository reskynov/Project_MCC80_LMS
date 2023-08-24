using API.Contracts;
using API.DTOs.Tasks;
using FluentValidation;

namespace API.Utilities.Validations.Tasks;

public class NewTaskValidator : AbstractValidator<NewTaskDto>
{
    private readonly ITaskRepository _taskRepository;

    public NewTaskValidator(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;

        RuleFor(t => t.UserGuid)
            .NotEmpty().WithMessage("User guid is required")
            .Must((dto, userGuid) => IsDuplicateValue(userGuid, dto.LessonGuid))
            .WithMessage("A task with the same UserGuid and LessonGuid already exists.");

        RuleFor(t => t.LessonGuid)
            .NotEmpty().WithMessage("Lesson guid is required")
            .Must((dto, lessonGuid) => IsDuplicateValue(dto.UserGuid, lessonGuid))
            .WithMessage("A task with the same UserGuid and LessonGuid already exists.");
    }

    private bool IsDuplicateValue(Guid userGuid, Guid lessonGuid)
    {
        return _taskRepository.IsNotExist(userGuid, lessonGuid);
    }
}
