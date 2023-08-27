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

        RuleFor(t => t.LessonGuid)
            .NotEmpty().WithMessage("Lesson guid is required")
            .WithMessage("A task with the same UserGuid and LessonGuid already exists.");
    }
}
