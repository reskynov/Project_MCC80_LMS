using API.Contracts;
using API.DTOs.UserClassrooms;
using FluentValidation;

namespace API.Utilities.Validations.UserClassrooms
{
    public class UserClassroomValidator : AbstractValidator<UserClassroomDto>
    {
        private readonly IUserClassroomRepository _userClassroomRepository;

        public UserClassroomValidator(IUserClassroomRepository userClassroomRepository)
        {
            RuleFor(uc => uc.UserGuid)
                .NotEmpty();

            RuleFor(uc => uc.ClassroomGuid)
                .NotEmpty();
        }
    }
}
