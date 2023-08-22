using API.DTOs.Classrooms;
using FluentValidation;

namespace API.Utilities.Validations.Classrooms
{
    public class EnrollClassroomValidator : AbstractValidator<EnrollClassroomDto>
    {
        public EnrollClassroomValidator() 
        {
            RuleFor(enroll => enroll.UserGuid)
                .NotEmpty();

            RuleFor(enroll => enroll.ClassroomCode)
                .NotEmpty()
                .MaximumLength(10).WithMessage("Class code more than maximum length");
        }
    }
}
