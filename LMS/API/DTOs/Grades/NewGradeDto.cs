using API.Models;

namespace API.DTOs.Grades;

public class NewGradeDto
{
    public int Value { get; set; }
    public Guid UserGuid { get; set; }
    public Guid TaskGuid { get; set; }

    public static implicit operator Grade(NewGradeDto newGradeDto)
    {
        return new Grade
        {
            Guid = new Guid(),
            Value = newGradeDto.Value,
            UserGuid = newGradeDto.UserGuid,
            TaskGuid = newGradeDto.TaskGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator NewGradeDto(Grade grade)
    {
        return new NewGradeDto
        {
            Value = grade.Value,
            UserGuid = grade.UserGuid,
            TaskGuid = grade.TaskGuid
        };
    }
}
