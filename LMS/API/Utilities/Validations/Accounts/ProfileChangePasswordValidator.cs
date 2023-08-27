using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Users;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class ProfileChangePasswordValidator : AbstractValidator<ProfileChangePasswordDto>
    {

        public ProfileChangePasswordValidator()
        {
            RuleFor(pass => pass.CurrentPassword)
                .NotEmpty()
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$").WithMessage("Password Must Contains minimum eight characters, " +
                                                                                                    "at least one uppercase letter, " +
                                                                                                    "one lowercase letter," +
                                                                                                    " one number and " +
                                                                                                    "one special character");

            RuleFor(pass => pass.NewPassword)
                .NotEmpty()
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$").WithMessage("Password Must Contains minimum eight characters, " +
                                                                                                    "at least one uppercase letter, " +
                                                                                                    "one lowercase letter," +
                                                                                                    " one number and " +
                                                                                                    "one special character");

            RuleFor(pass => pass.ConfirmNewPassword)
                .Equal(pass => pass.NewPassword)
                .WithMessage("Passwords does not match");
        }

    }
}