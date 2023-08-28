namespace API.DTOs.Lessons
{
    public class UpdateLessonTaskDto
    {
        public Guid LessonGuid { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? SubjectAttachment { get; set; }
        public DateTime? DeadlineDate { get; set; }
    }
}
