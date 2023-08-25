namespace API.DTOs.Classrooms
{
    public class ClassroomByCodeDto
    {
        public Guid ClassroomGuid { get; set; }
        public string ClassroomName { get; set; }
        public string TeacherName { get; set; }
        public int PeopleCount { get; set; }
    }
}
