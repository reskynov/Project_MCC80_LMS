using Client.Models;

namespace Client.ViewModels.UserClassrooms
{
    public class NewUserClassroomVM
    {
        public Guid UserGuid { get; set; }
        public Guid ClassroomGuid { get; set; }

        public static implicit operator UserClassroom(NewUserClassroomVM newUserClassroomDto)
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

        public static explicit operator NewUserClassroomVM(UserClassroom userClassroom)
        {
            return new NewUserClassroomVM
            {
                UserGuid = userClassroom.UserGuid,
                ClassroomGuid = userClassroom.ClassroomGuid
            };
        }
    }
}
