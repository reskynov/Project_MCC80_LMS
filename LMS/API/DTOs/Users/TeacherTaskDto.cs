using API.DTOs.Lessons;

namespace API.DTOs.Users
{
    public class TeacherTaskDto
    {
        public string ClassroomName { get; set; }
        public Guid LessonGuid { get; set; }
        public string LessonName { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public int TotalGraded { get; set; }
        public int TotalNotGraded { get; set; }
        public int TotalNotSubmitted { get; set; }

        //public IEnumerable<TeacherTaskInClassDto> TaskInClassroom { get; set; }
    }
}
