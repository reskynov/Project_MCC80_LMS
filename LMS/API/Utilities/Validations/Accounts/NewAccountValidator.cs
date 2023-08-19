using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class NewAccountValidator : AbstractValidator<NewAccountDto>
    {
        public NewAccountValidator()
        {
            RuleFor(a => a.Password)
                .NotEmpty()
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$");

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
