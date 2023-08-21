using Client.Models;

namespace Client.ViewModels.Classrooms;

public class ClassroomVM
{
    public Guid Guid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public static implicit operator Classroom(ClassroomVM classroomDto)
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

    public static explicit operator ClassroomVM(Classroom classroom)
    {
        return new ClassroomVM
        {
            Guid = classroom.Guid,
            Code = classroom.Code,
            Name = classroom.Name,
            Description = classroom.Description
        };
    }
}
