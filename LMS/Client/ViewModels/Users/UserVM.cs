using Client.Models;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.Users
{
    public class UserVM
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

        [RegularExpression(@"^\+[0-9]", ErrorMessage = "Phone number need to start with '+' and input only number")]
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
