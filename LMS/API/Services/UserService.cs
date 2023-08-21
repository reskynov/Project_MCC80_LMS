using API.Contracts;
using API.DTOs.Users;
using API.Models;

namespace API.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IUserClassroomRepository _userClassroomRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;

        public UserService(IUserRepository userRepository, 
            IClassroomRepository classroomRepository, 
            IUserClassroomRepository userClassroomRepository,
            IRoleRepository roleRepository,
            IAccountRoleRepository accountRoleRepository)
        {
            _userRepository = userRepository;
            _classroomRepository = classroomRepository;
            _userClassroomRepository = userClassroomRepository;
            _roleRepository = roleRepository;
            _accountRoleRepository = accountRoleRepository;
        }

        public IEnumerable<ClassroomByUserDto> GetCLassroomByUser(Guid guid)
        {
            var getClassroomByUser = from c in _classroomRepository.GetAll()
                                     join uc in _userClassroomRepository.GetAll() on c.Guid equals uc.ClassroomGuid
                                     join u in _userRepository.GetAll() on uc.UserGuid equals u.Guid
                                     where u.Guid == guid
                                     select new ClassroomByUserDto
                                     {
                                         ClassroomGuid = c.Guid,
                                         ClassroomName = c.Name
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
