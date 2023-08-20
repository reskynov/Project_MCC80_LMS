using API.Contracts;
using API.DTOs.Users;
using FluentValidation;

namespace API.Utilities.Validations.Users
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository)
        {
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
                .Must(IsDuplicateOrSame).WithMessage("Email already exists");

            RuleFor(u => u.PhoneNumber)
                .MaximumLength(20)
                .Matches(@"^\+[0-9]").WithMessage("Phone number must start with +")
                .Must(IsDuplicateOrSame).WithMessage("Email already exists")
                .When(u => !string.IsNullOrEmpty(u.PhoneNumber));
        }

        private bool IsDuplicateOrSame(string arg)
        {
            return _userRepository.IsDataUnique(arg);
        }
    }
}
