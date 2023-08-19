using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

public class RoleValidator : AbstractValidator<RoleDto>
{
    public RoleValidator()
    {
        RuleFor(r => r.Guid)
            .NotEmpty().WithMessage("Guid is required");

        RuleFor(r => r.Name)
         .NotEmpty().WithMessage("Name is required");
    }
}
