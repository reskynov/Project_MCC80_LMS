namespace API.DTOs.Lessons
{
    public class LessonDetailDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? SubjectAttachment { get; set; }
        public bool IsTask { get; set; }
        public Guid ClassroomGuid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
    }
}
