using API.Contracts;
using API.DTOs.UserTasks;
using FluentValidation;

namespace API.Utilities.Validations.UserTasks;

public class UserTaskValidator : AbstractValidator<UserTaskDto>
{
    private readonly IUserTaskRepository _userTaskRepository;
    public UserTaskValidator(IUserTaskRepository userTaskRepository)
    {
        _userTaskRepository = userTaskRepository;

        RuleFor(g => g.Guid)
            .NotEmpty().WithMessage("Guid is required");

        RuleFor(g => g.Attachment)
            .NotEmpty().WithMessage("Attachment is required");

        RuleFor(g => g.Grade)
            .InclusiveBetween(0, 100).WithMessage("Value must be between 0 and 100.");

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
        return _userTaskRepository.IsDataUnique(userGuid, taskGuid);
    }
}
