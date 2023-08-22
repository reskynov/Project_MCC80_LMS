namespace API.DTOs.Users
{
    public class ClassroomByUserDto
    {
        public Guid ClassroomGuid { get; set; }
        public string ClassroomName { get; set; }
        public Guid TeacherGuid { get; set; }
    }
}
