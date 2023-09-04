namespace Client.ViewModels.Classrooms;

public class ClassroomLessonVM
{
    public Guid ClassroomGuid { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? SubjectAttachment { get; set; }
    public bool isTask { get; set; }    
    public DateTime CreatedDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public Guid LessonGuid { get; set; }
    public DateTime? StudentSubmittedDate { get; set; }

}
