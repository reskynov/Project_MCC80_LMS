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
            .NotEmpty().WithMessage("User guid is required")
            .Must((dto, userGuid) => IsDuplicateOrSame(userGuid, dto.ClassroomGuid))
            .WithMessage("A UserClassroom with the same UserGuid and ClassroomGuid already exists.");

        RuleFor(uc => uc.ClassroomGuid)
            .NotEmpty().WithMessage("Classroom guid is required")
            .Must((dto, classroomGuid) => IsDuplicateOrSame(dto.UserGuid, classroomGuid))
            .WithMessage("A UserClassroom with the same UserGuid and ClassroomGuid already exists.");
    }

    private bool IsDuplicateOrSame(Guid userGuid, Guid classroomGuid)
    {
        return _userClassroomRepository.IsDataUnique(userGuid, classroomGuid);
    }
}
