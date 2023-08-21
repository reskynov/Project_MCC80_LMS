using Client.Models;

namespace Client.ViewModels.Grades;

public class NewGradeVM
{
    public int Value { get; set; }
    public Guid UserGuid { get; set; }
    public Guid TaskGuid { get; set; }

    public static implicit operator Grade(NewGradeVM newGradeDto)
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

    public static explicit operator NewGradeVM(Grade grade)
    {
        return new NewGradeVM
        {
            Value = grade.Value,
            UserGuid = grade.UserGuid,
            TaskGuid = grade.TaskGuid
        };
    }
}
