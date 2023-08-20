using API.Contracts;
using API.DTOs.UserClassrooms;
using FluentValidation;

namespace API.Utilities.Validations.UserClassrooms;

public class NewUserClassroomValidator : AbstractValidator<NewUserClassroomDto>
{
    private readonly IUserClassroomRepository _userClassroomRepository;

    public  NewUserClassroomValidator(IUserClassroomRepository userClassroomRepository)
    {
        _userClassroomRepository = userClassroomRepository;

        RuleFor(uc => uc.UserGuid)
            .NotEmpty().WithMessage("User guid is required")
            .Must((dto, userGuid) => IsDuplicateValue(userGuid, dto.ClassroomGuid))
            .WithMessage("A UserClassroom with the same UserGuid and ClassroomGuid already exists.");

        RuleFor(uc => uc.ClassroomGuid)
            .NotEmpty().WithMessage("Classroom guid is required")
            .Must((dto, classroomGuid) => IsDuplicateValue(dto.UserGuid, classroomGuid))
            .WithMessage("A UserClassroom with the same UserGuid and ClassroomGuid already exists.");
    }

    // Update IsDuplicateValue to take two string arguments
    private bool IsDuplicateValue(Guid userGuid, Guid classroomGuid)
    {
        return _userClassroomRepository.IsNotExist(userGuid, classroomGuid);
    }
}
