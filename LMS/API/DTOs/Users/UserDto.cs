using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Users
{
    public class UserDto
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public static implicit operator User(UserDto userDto)
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

        public static explicit operator UserDto(User user)
        {
            return new UserDto
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
