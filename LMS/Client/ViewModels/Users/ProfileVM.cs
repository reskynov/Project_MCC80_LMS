using Client.Utilities.Enums;
using Client.ViewModels.Accounts;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.Users
{
    public class ProfileVM
    {
        public Guid Guid { get; set; }

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

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Current Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contains minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contains minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords should be the same.")]
        public string ConfirmNewPassword { get; set; }
    }
}
