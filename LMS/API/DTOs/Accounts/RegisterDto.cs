using API.Utilities.Enums;

namespace API.DTOs.Accounts
{
    public class RegisterDto
    {
        //Account
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        //User
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

        //Role
        public Guid RoleGuid { get; set; }
    }
}
