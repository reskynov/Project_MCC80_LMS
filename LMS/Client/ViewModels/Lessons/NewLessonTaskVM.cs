using Client.Models;

namespace Client.ViewModels.Lessons;

public class NewLessonTaskVM
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public IFormFile SubjectAttachmentFile { get; set; }
    public string? SubjectAttachment { get; set; }
    public bool IsTask { get; set; }
    public Guid ClassroomGuid { get; set; }
    public DateTime? DeadlineDate { get; set; }
}
