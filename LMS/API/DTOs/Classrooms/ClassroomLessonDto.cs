namespace API.DTOs.Classrooms;

public class ClassroomLessonDto
{
    public Guid ClassroomGuid { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? SubjectAttachment { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid LessonGuid { get; set; }
}
