using API.DTOs.Accounts;
using API.Utilities.Enums;

namespace API.DTOs.Users
{
    public class ProfileDto
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set;}
    }
}
