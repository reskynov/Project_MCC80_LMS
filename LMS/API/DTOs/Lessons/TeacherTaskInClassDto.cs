namespace API.DTOs.Lessons
{
    public class TeacherTaskInClassDto
    {
        public Guid LessonGuid { get; set; }
        public string LessonName { get; set; }
        public DateTime? Deadline { get; set; }
        public Guid TaskGuid { get; set; }
    }
}
