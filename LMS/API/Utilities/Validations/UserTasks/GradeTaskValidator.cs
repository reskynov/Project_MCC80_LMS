using API.Contracts;
using API.DTOs.UserTasks;
using FluentValidation;

namespace API.Utilities.Validations.UserTasks;

public class GradeTaskValidator : AbstractValidator<GradeTaskDto>
{
    private readonly IUserTaskRepository _gradeRepository;
    public GradeTaskValidator(IUserTaskRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;

        RuleFor(g => g.Grade)
            .InclusiveBetween(0, 100).WithMessage("Value must be between 0 and 100.");
    }

    // Update IsDuplicateValue to take two string arguments
    private bool IsDuplicateValue(Guid userGuid, Guid taskGuid)
    {
        return _gradeRepository.IsNotExist(userGuid, taskGuid);
    }

}
