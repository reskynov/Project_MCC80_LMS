using API.DTOs.Lessons;

namespace API.DTOs.Users
{
    public class TeacherTaskDto
    {
        public string ClassroomName { get; set; }

        public IEnumerable<TeacherTaskInClassDto> TaskInClassroom { get; set; }
    }
}
