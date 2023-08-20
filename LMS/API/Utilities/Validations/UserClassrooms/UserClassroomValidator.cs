using API.Contracts;
using API.DTOs.UserClassrooms;
using FluentValidation;

namespace API.Utilities.Validations.UserClassrooms;

public class UserClassroomValidator : AbstractValidator<UserClassroomDto>
{
    private readonly IUserClassroomRepository _userClassroomRepository;

    public UserClassroomValidator(IUserClassroomRepository userClassroomRepository)
    {
        _userClassroomRepository = userClassroomRepository;

        RuleFor(uc => uc.UserGuid)
            .NotEmpty().WithMessage("User guid is required");

        RuleFor(uc => uc.ClassroomGuid)
            .NotEmpty().WithMessage("Classroom guid is required");
    }
}
