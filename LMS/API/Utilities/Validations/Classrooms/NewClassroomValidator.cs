using API.DTOs.Classrooms;
using FluentValidation;

namespace API.Utilities.Validations.Classrooms
{
    public class NewClassroomValidator : AbstractValidator<NewClassroomDto>
    {
        public NewClassroomValidator() 
        {
            RuleFor(c => c.Name)
                .NotEmpty();
        }

    }
}
