﻿using Client.Models;
using Client.Utilities.Enums;

namespace Client.ViewModels.Users
{
    public class NewUserVM
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public static implicit operator User(NewUserVM newUserDto)
        {
            return new User
            {
                Guid = new Guid(),
                FirstName = newUserDto.FirstName,
                LastName = newUserDto.LastName,
                Gender = newUserDto.Gender,
                BirthDate = newUserDto.BirthDate,
                Email = newUserDto.Email,
                PhoneNumber = newUserDto.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewUserVM(User user)
        {
            return new NewUserVM
            {
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
