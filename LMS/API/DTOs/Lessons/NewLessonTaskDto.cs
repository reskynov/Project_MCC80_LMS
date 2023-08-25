using API.Models;

namespace API.DTOs.Lessons;

public class NewLessonTaskDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? SubjectAttachment { get; set; }
    public bool IsTask { get; set; }
    public Guid ClassroomGuid { get; set; }
    public DateTime? DeadlineDate { get; set; }
}
