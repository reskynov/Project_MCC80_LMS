using API.Contracts;
using API.DTOs.Users;
using API.Models;

namespace API.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
