using Client.Models;
using Client.Utilities.Enums;

namespace Client.ViewModels.Users
{
    public class UserVM
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public static implicit operator User(UserVM userDto)
        {
            return new User
            {
                Guid = userDto.Guid,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Gender = userDto.Gender,
                BirthDate = userDto.BirthDate,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator UserVM(User user)
        {
            return new UserVM
            {
                Guid = user.Guid,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                BirthDate = user.BirthDate,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}
