using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.Grades;

public class GradeDto
{
    public Guid Guid { get; set; }
    public int Value { get; set; }
    public Guid UserGuid { get; set; }
    public Guid TaskGuid { get; set; }

    public static implicit operator Grade(GradeDto gradeDto)
    {
        return new Grade
        {
            Guid = gradeDto.Guid,
            Value = gradeDto.Value,
            UserGuid = gradeDto.UserGuid,
            TaskGuid = gradeDto.TaskGuid
        };
    }

    public static explicit operator GradeDto(Grade grade)
    {
        return new GradeDto
        {
            Guid = grade.Guid,
            Value = grade.Value,
            UserGuid = grade.UserGuid,
            TaskGuid = grade.TaskGuid
        };
    }
}
