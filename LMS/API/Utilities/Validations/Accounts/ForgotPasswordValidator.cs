using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required")
                .MaximumLength(45).WithMessage("Email more than maximum length")
                .EmailAddress().WithMessage("Email is not valid");
        }
    }
}
