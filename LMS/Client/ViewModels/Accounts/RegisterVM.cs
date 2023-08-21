using Client.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.Accounts
{
    public class RegisterVM
    {
        //Account
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

        //User
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public GenderLevel Gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

        //Role
        [Required]
        public Guid RoleGuid { get; set; }
    }
}
