using API.Models;

namespace API.DTOs.UserClassrooms
{
    public class NewUserClassroomDto
    {
        public Guid UserGuid { get; set; }
        public Guid ClassroomGuid { get; set; }

        public static implicit operator UserClassroom(NewUserClassroomDto newUserClassroomDto)
        {
            return new UserClassroom
            {
                Guid = new Guid(),
                UserGuid = newUserClassroomDto.UserGuid,
                ClassroomGuid = newUserClassroomDto.ClassroomGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewUserClassroomDto(UserClassroom userClassroom)
        {
            return new NewUserClassroomDto
            {
                UserGuid = userClassroom.UserGuid,
                ClassroomGuid = userClassroom.ClassroomGuid
            };
        }
    }
}
