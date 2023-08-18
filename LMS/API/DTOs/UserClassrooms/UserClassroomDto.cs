using API.Models;

namespace API.DTOs.UserClassrooms
{
    public class UserClassroomDto
    {
        public Guid Guid { get; set; }
        public Guid UserGuid { get; set; }
        public Guid ClassroomGuid { get; set; }

        public static implicit operator UserClassroom(UserClassroomDto userClassroomDto)
        {
            return new UserClassroom
            {
                Guid = userClassroomDto.Guid,
                UserGuid = userClassroomDto.UserGuid,
                ClassroomGuid = userClassroomDto.ClassroomGuid,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator UserClassroomDto(UserClassroom userClassroom)
        {
            return new UserClassroomDto
            {
                Guid = userClassroom.Guid,
                UserGuid = userClassroom.UserGuid,
                ClassroomGuid = userClassroom.ClassroomGuid
            };
        }
    }
}
