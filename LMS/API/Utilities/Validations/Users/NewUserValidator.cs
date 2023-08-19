using API.Contracts;
using API.DTOs.Users;
using FluentValidation;

namespace API.Utilities.Validations.Users
{
    public class NewUserValidator : AbstractValidator<NewUserDto>
    {
        private readonly IUserRepository _userRepository;

        public NewUserValidator(IUserRepository userRepository)
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
                .Must(IsDuplicationValue).WithMessage("Email already exist");

            //RuleFor(u => u.PhoneNumber)
            //    .MaximumLength(20)
            //When(u => u.PhoneNumber != null, () =>
            //    {
            //        RuleFor(u => u.PhoneNumber)
            //        .MaximumLength(20);

            //        RuleFor(u => u.PhoneNumber)
            //        .Matches(@"^\+[0-9]").WithMessage("Phone number must start with +");

            //        RuleFor(u => u.PhoneNumber)
            //        .Must(IsDuplicationValue).WithMessage("Phone number already exist");
            //    });

            //When(u => u.PhoneNumber is "", () => 
            //{
            //    RuleFor(u => u.PhoneNumber).Null();
            //});

            RuleFor(u => u.PhoneNumber)
                .MaximumLength(20)
                .Matches(@"^\+[0-9]").WithMessage("Phone number must start with +")
                .Must(IsDuplicationValue).WithMessage("Phone number already exist")
                .When(u => !string.IsNullOrEmpty(u.PhoneNumber));
        }

        private bool IsDuplicationValue(string arg)
        {
            return _userRepository.IsNotExist(arg);
        }


    }
}
