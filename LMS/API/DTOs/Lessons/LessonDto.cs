using API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.DTOs.Lessons;

public class LessonDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? SubjectAttachment { get; set; }
    public Guid ClassroomGuid { get; set; }

    public static implicit operator Lesson(LessonDto lessonDto)
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

    public static explicit operator LessonDto(Lesson lesson)
    {
        return new LessonDto
        {
            Guid = lesson.Guid,
            Name = lesson.Name,
            Description = lesson.Description,
            SubjectAttachment = lesson.SubjectAttachment,
            ClassroomGuid = lesson.ClassroomGuid
        };
    }
}
