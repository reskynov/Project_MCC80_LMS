using API.Contracts;
using API.DTOs.Tasks;
using FluentValidation;

namespace API.Utilities.Validations.Tasks
{
    public class NewTaskValidator : AbstractValidator<NewTaskDto>
    {
        private readonly ITaskRepository _taskRepository;

        public NewTaskValidator(ITaskRepository taskRepository)
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
