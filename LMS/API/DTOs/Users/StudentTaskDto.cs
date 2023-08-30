using API.DTOs.Lessons;

namespace API.DTOs.Users
{
    public class TeacherTaskDto
    {
        public string ClassroomName { get; set; }

        public TeacherTaskInClassDto Classroom { get; set; }
    }
}
