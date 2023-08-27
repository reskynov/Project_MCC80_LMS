using Client.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.Accounts
{
    public class RegisterVM
    {
        //Account
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contains minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords should be the same.")]
        public string ConfirmPassword { get; set; }

        //User
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please choose your gender")]
        public GenderLevel Gender { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        
        [RegularExpression(@"^\+[0-9]", ErrorMessage = "Phone number need to start with '+' and input only number")]
        public string? PhoneNumber { get; set; }

        //Role
        [Required]
        public Guid RoleGuid { get; set; }
    }
}
