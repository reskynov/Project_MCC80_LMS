using Client.Models;
namespace Client.ViewModels.Lessons;

public class LessonVM
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? SubjectAttachment { get; set; }
    public Guid ClassroomGuid { get; set; }

    public static implicit operator Lesson(LessonVM lessonDto)
    {
        return new Lesson
        {
            Guid = lessonDto.Guid,
            Name = lessonDto.Name,
            Description = lessonDto.Description,
            SubjectAttachment = lessonDto.SubjectAttachment,
            ClassroomGuid = lessonDto.ClassroomGuid,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator LessonVM(Lesson lesson)
    {
        return new LessonVM
        {
            Guid = lesson.Guid,
            Name = lesson.Name,
            Description = lesson.Description,
            SubjectAttachment = lesson.SubjectAttachment,
            ClassroomGuid = lesson.ClassroomGuid
        };
    }
}
