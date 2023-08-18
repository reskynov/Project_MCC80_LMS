using API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.Classrooms;

public class ClassroomDto
{
    public Guid Guid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public static implicit operator Classroom(ClassroomDto classroomDto)
    {
        return new Classroom
        {
            Guid = classroomDto.Guid,
            Code = classroomDto.Code,
            Name = classroomDto.Name,
            Description = classroomDto.Description,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator ClassroomDto(Classroom classroom)
    {
        return new ClassroomDto
        {
            Guid = classroom.Guid,
            Code = classroom.Code,
            Name = classroom.Name,
            Description = classroom.Description
        };
    }
}
