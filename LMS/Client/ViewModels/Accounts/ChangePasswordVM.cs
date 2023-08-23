using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.Accounts
{
    public class ChangePasswordVM
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contains minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords should be the same.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "OTP Code is required")]
        public int OTP { get; set; }
    }
}
