using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        private readonly IUserRepository _userRepository;
        public RegisterValidator(IUserRepository userRepository)
        {
            //user repository
            _userRepository = userRepository;

            RuleFor(u => u.FirstName)
                          .NotEmpty()
                          .MaximumLength(100).WithMessage("First Name more than maximum length");

            RuleFor(u => u.Gender)
                .NotNull()
                .IsInEnum();

            RuleFor(u => u.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-10));

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required")
                .MaximumLength(45).WithMessage("Email more than maximum length")
                .EmailAddress().WithMessage("Email is not valid")
                .Must(IsDuplicationValue).WithMessage("Email already exist");

            RuleFor(u => u.PhoneNumber)
                .MaximumLength(20)
                .Matches(@"^\+[0-9]").WithMessage("Phone number must start with +")
                .Must(IsDuplicationValue).WithMessage("Phone number already exist");

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

            RuleFor(r => r.RoleGuid)
                .NotEmpty();
        }

        private bool IsDuplicationValue(string arg)
        {
            return _userRepository.IsNotExist(arg);
        }
    }
}
