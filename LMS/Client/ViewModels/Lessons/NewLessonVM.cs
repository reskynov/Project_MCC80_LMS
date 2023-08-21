using Client.Models;

namespace Client.ViewModels.Lessons;

public class NewLessonVM
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? SubjectAttachment { get; set; }
    public Guid ClassroomGuid { get; set; }

    public static implicit operator Lesson(NewLessonVM newLessonDto)
    {
        return new Lesson
        {
            Guid = new Guid(),
            Name = newLessonDto.Name,
            Description = newLessonDto.Description,
            SubjectAttachment = newLessonDto.SubjectAttachment,
            ClassroomGuid = newLessonDto.ClassroomGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator NewLessonVM(Lesson lesson)
    {
        return new NewLessonVM
        {
            Name = lesson.Name,
            Description = lesson.Description,
            SubjectAttachment = lesson.SubjectAttachment,
            ClassroomGuid = lesson.ClassroomGuid
        };
    }
}
