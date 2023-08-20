using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required")
                .MaximumLength(45).WithMessage("Email more than maximum length")
                .EmailAddress().WithMessage("Email is not valid");

            //Account Data
            RuleFor(pass => pass.Password)
                .NotEmpty()
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$").WithMessage("Password Must Contains minimum eight characters, " +
                                                                                                    "at least one uppercase letter, " +
                                                                                                    "one lowercase letter," +
                                                                                                    " one number and " +
                                                                                                    "one special character");

            RuleFor(pass => pass.ConfirmPassword)
                .Equal(pass => pass.Password)
                .WithMessage("Passwords do not match");

            RuleFor(a => a.OTP)
                .NotEmpty();
        }
    }
}
