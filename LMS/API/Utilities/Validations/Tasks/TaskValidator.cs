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

        RuleFor(t => t.LessonGuid)
            .NotEmpty().WithMessage("Lesson guid is required")
            .WithMessage("A task with the same UserGuid and LessonGuid already exists.");
    }
}
