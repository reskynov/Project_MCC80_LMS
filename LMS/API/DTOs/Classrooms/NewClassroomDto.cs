using API.Models;
using API.Utilities.Handlers;

namespace API.DTOs.Classrooms;

public class NewClassroomDto
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public static implicit operator Classroom(NewClassroomDto newClassroomDto)
    {
        return new Classroom
        {
            Guid = new Guid(),
            Code = GenerateHandler.ClassCode(),
            Name = newClassroomDto.Name,
            Description = newClassroomDto.Description,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator NewClassroomDto(Classroom classroom)
    {
        return new NewClassroomDto
        {
            Name = classroom.Name,
            Description = classroom.Description
        };
    }
}
