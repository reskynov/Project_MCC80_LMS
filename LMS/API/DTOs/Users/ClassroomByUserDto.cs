namespace API.DTOs.Users
{
    public class ClassroomByUserDto
    {
        public Guid ClassroomGuid { get; set; }
        public string ClassroomName { get; set; }
        public string TeacherName { get; set; }
        public Guid UserClassroomGuid { get; set; }
        public int StudentCount { get; set; }
    }
}
