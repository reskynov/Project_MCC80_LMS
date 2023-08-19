using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Classrooms
{
    public class ClassroomValidator : AbstractValidator<AccountDto>
    {
        public ClassroomValidator() { }
    }
}
