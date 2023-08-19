using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class AccountValidator : AbstractValidator<AccountDto>
    {
        public AccountValidator()
        {
            RuleFor(a => a.Password)
                .NotEmpty()
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$").WithMessage("Password Must Contains minimum eight characters, " +
                                                                                                    "at least one uppercase letter, " +
                                                                                                    "one lowercase letter," +
                                                                                                    " one number and " +
                                                                                                    "one special character");

            RuleFor(a => a.ExpiredDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now);

            RuleFor(a => a.OTP)
                .NotEmpty();

            RuleFor(a => a.IsUsed)
                .NotEmpty();
        }
    }
}
