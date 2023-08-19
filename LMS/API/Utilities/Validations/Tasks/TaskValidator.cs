using API.Contracts;
using API.DTOs.Tasks;
using FluentValidation;

namespace API.Utilities.Validations.Tasks
{
    public class TaskValidator : AbstractValidator<TaskDto>
    {
        private readonly ITaskRepository _taskRepository;

        public TaskValidator(ITaskRepository taskRepository)
        {
            RuleFor(t => t.Attachment)
                .NotEmpty();

            RuleFor(t => t.UserGuid)
                .NotEmpty();

            RuleFor(t => t.LessonGuid)
                .NotEmpty();
        }
    }
}
