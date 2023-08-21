using Client.Models;

namespace Client.ViewModels.UserClassrooms
{
    public class UserClassroomVM
    {
        public Guid Guid { get; set; }
        public Guid UserGuid { get; set; }
        public Guid ClassroomGuid { get; set; }

        public static implicit operator UserClassroom(UserClassroomVM userClassroomDto)
        {
            return new UserClassroom
            {
                Guid = userClassroomDto.Guid,
                UserGuid = userClassroomDto.UserGuid,
                ClassroomGuid = userClassroomDto.ClassroomGuid,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator UserClassroomVM(UserClassroom userClassroom)
        {
            return new UserClassroomVM
            {
                Guid = userClassroom.Guid,
                UserGuid = userClassroom.UserGuid,
                ClassroomGuid = userClassroom.ClassroomGuid
            };
        }
    }
}
