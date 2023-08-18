using API.Models;

namespace API.DTOs.Classrooms;

public class NewClassroomDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public static implicit operator Classroom(NewClassroomDto newClassroomDto)
    {
        return new Classroom
        {
            Guid = new Guid(),
            Code = newClassroomDto.Code,
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
            Code = classroom.Code,
            Name = classroom.Name,
            Description = classroom.Description
        };
    }
}
