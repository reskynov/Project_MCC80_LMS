using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles
{
    public class NewAccountRoleValidator : AbstractValidator<NewAccountRoleDto>
    {
        public NewAccountRoleValidator()
        {
            RuleFor(acc => acc.AccountGuid)
                .NotEmpty();

            RuleFor(acc => acc.RoleGuid)
                .NotEmpty();
        }
    }
}
