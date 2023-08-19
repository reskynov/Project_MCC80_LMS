using API.Contracts;
using API.DTOs.UserClassrooms;
using API.Models;

namespace API.Services
{
    public class UserClassroomService
    {
        private readonly IUserClassroomRepository _userClassroomRepository;

        public UserClassroomService(IUserClassroomRepository userClassroomRepository)
        {
            _userClassroomRepository = userClassroomRepository;
        }

        public IEnumerable<UserClassroomDto> GetAll()
        {
            var userClassrooms = _userClassroomRepository.GetAll();
            if (!userClassrooms.Any())
            {
                return Enumerable.Empty<UserClassroomDto>();
            }

            var userClassroomDtos = new List<UserClassroomDto>();
            foreach (var userClassroom in userClassrooms)
            {
                userClassroomDtos.Add((UserClassroomDto)userClassroom);
            }

            return userClassroomDtos;
        }

        public UserClassroomDto? GetByGuid(Guid guid)
        {
            var userClassroom = _userClassroomRepository.GetByGuid(guid);
            if (userClassroom is null)
            {
                return null;
            }

            return (UserClassroomDto)userClassroom;
        }

        public UserClassroomDto? Create(NewUserClassroomDto newUserClassroomDto)
        {
            var userClassroom = _userClassroomRepository.Create(newUserClassroomDto);
            if (userClassroom is null)
            {
                return null;
            }

            return (UserClassroomDto)userClassroom;
        }

        public int Update(UserClassroomDto userClassroomDto)
        {
            var userClassroom = _userClassroomRepository.GetByGuid(userClassroomDto.Guid);
            if (userClassroom is null)
            {
                return -1;
            }

            UserClassroom toUpdate = userClassroomDto;
            toUpdate.CreatedDate = userClassroom.CreatedDate;

            var result = _userClassroomRepository.Update(toUpdate);
            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var userClassroom = _userClassroomRepository.GetByGuid(guid);

            if (userClassroom is null)
            {
                return -1;
            }

            var result = _userClassroomRepository.Delete(userClassroom);
            return result ? 1 : 0;
        }
    }
}
