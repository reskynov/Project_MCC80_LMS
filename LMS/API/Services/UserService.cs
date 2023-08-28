using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Users;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IUserClassroomRepository _userClassroomRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;

        public UserService(IUserRepository userRepository, 
            IClassroomRepository classroomRepository, 
            IUserClassroomRepository userClassroomRepository,
            IRoleRepository roleRepository,
            IAccountRoleRepository accountRoleRepository,
            IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _classroomRepository = classroomRepository;
            _userClassroomRepository = userClassroomRepository;
            _roleRepository = roleRepository;
            _accountRoleRepository = accountRoleRepository;
            _accountRepository = accountRepository;
        }

        public int ChangePassword(ProfileChangePasswordDto profileChangePasswordDto)
        {
            var getAccount = (from  a in _accountRepository.GetAll()
                              where a.Guid == profileChangePasswordDto.GuidAccount
                              select a).FirstOrDefault();

            if (getAccount is null)
            {
                return 0; //account not found
            }

            if(!HashingHandler.ValidateHash(profileChangePasswordDto.CurrentPassword, getAccount.Password))
            {
                return -1;
            }

            var account = new Account
            {
                Guid = getAccount.Guid,
                IsUsed = true,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccount.CreatedDate,
                OTP = getAccount.OTP,
                ExpiredDate = getAccount.ExpiredDate,
                Password = HashingHandler.GenerateHash(profileChangePasswordDto.NewPassword),
            };

            _accountRepository.Clear();

            var isUpdated = _accountRepository.Update(account);
            if (!isUpdated)
            {
                return -2; //Account not Update
            }

            return 1;
        }

        public ProfileDto GetProfile(Guid guid)
        {
            var getUser = (from u in _userRepository.GetAll()
                                     where u.Guid == guid
                                     select new ProfileDto
                                     {
                                         Guid = u.Guid,
                                         FirstName = u.FirstName,
                                         LastName = u.LastName,
                                         BirthDate = u.BirthDate,
                                         Gender = u.Gender,
                                         Email = u.Email,
                                         PhoneNumber = u.PhoneNumber
                                     }).SingleOrDefault();

            if (getUser is null)
            {
                return null;
            }

            return getUser;
        }

        public IEnumerable<ClassroomByUserDto> GetCLassroomByUser(Guid guid)
        {
            var getClassroomByUser = from c in _classroomRepository.GetAll()
                                     join uc in _userClassroomRepository.GetAll() on c.Guid equals uc.ClassroomGuid
                                     join u in _userRepository.GetAll() on uc.UserGuid equals u.Guid
                                     join t in _userRepository.GetAll() on c.TeacherGuid equals t.Guid
                                     where u.Guid == guid
                                     select new ClassroomByUserDto
                                     {
                                         ClassroomGuid = c.Guid,
                                         ClassroomName = c.Name,
                                         TeacherName = t.FirstName + " " + t.LastName,
                                         UserClassroomGuid = uc.Guid,
                                         StudentCount = _userClassroomRepository.GetAll().Where(uc => uc.ClassroomGuid == c.Guid && uc.UserGuid != guid).Count()
                                     };

            if (!getClassroomByUser.Any())
            {
                return Enumerable.Empty<ClassroomByUserDto>();
            }

            return getClassroomByUser;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _userRepository.GetAll();
            if (!users.Any())
            {
                return Enumerable.Empty<UserDto>();
            }

            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                userDtos.Add((UserDto)user);
            }

            return userDtos;
        }

        public UserDto? GetByGuid(Guid guid)
        {
            var user = _userRepository.GetByGuid(guid);
            if (user is null)
            {
                return null;
            }

            return (UserDto)user;
        }

        public UserDto? Create(NewUserDto newUserDto)
        {
            var user = _userRepository.Create(newUserDto);
            if (user is null)
            {
                return null;
            }

            return (UserDto)user;
        }

        public int Update(UserDto userDto)
        {
            var user = _userRepository.GetByGuid(userDto.Guid);

            if (user is null)
            {
                return -1;
            }

            User toUpdate = userDto;
            toUpdate.CreatedDate = user.CreatedDate;

            var result = _userRepository.Update(toUpdate);
            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var user = _userRepository.GetByGuid(guid);

            if (user is null)
            {
                return -1;
            }

            var result = _userRepository.Delete(user);
            return result ? 1 : 0;
        }
    }
}
