using API.Contracts;
using API.DTOs.UserClassrooms;
using FluentValidation;

namespace API.Utilities.Validations.UserClassrooms
{
    public class NewUserClassroomValidator : AbstractValidator<NewUserClassroomDto>
    {
        private readonly IUserClassroomRepository _userClassroomRepository;

        public  NewUserClassroomValidator(IUserClassroomRepository userClassroomRepository)
        {
            RuleFor(uc => uc.UserGuid)
                .NotEmpty();

            RuleFor(uc => uc.ClassroomGuid)
                .NotEmpty();
        }
    }
}
