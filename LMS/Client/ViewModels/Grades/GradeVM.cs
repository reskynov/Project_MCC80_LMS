using Client.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.ViewModels.Grades;

public class GradeVM
{
    public Guid Guid { get; set; }
    public int Value { get; set; }
    public Guid UserGuid { get; set; }
    public Guid TaskGuid { get; set; }

    public static implicit operator Grade(GradeVM gradeDto)
    {
        return new Grade
        {
            Guid = gradeDto.Guid,
            Value = gradeDto.Value,
            UserGuid = gradeDto.UserGuid,
            TaskGuid = gradeDto.TaskGuid
        };
    }

    public static explicit operator GradeVM(Grade grade)
    {
        return new GradeVM
        {
            Guid = grade.Guid,
            Value = grade.Value,
            UserGuid = grade.UserGuid,
            TaskGuid = grade.TaskGuid
        };
    }
}
