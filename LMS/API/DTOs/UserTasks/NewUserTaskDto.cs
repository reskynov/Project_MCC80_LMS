using API.Models;

namespace API.DTOs.UserTasks;

public class NewUserTaskDto
{
    public string Attachment { get; set; }
    public int? Grade { get; set; }
    public Guid UserGuid { get; set; }
    public Guid TaskGuid { get; set; }

    public static implicit operator UserTask(NewUserTaskDto newUserTaskDto)
    {
        return new UserTask
        {
            Guid = new Guid(),
            Attachment = newUserTaskDto.Attachment,
            Grade = newUserTaskDto.Grade,
            UserGuid = newUserTaskDto.UserGuid,
            TaskGuid = newUserTaskDto.TaskGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator NewUserTaskDto(UserTask userTask)
    {
        return new NewUserTaskDto
        {
            Attachment = userTask.Attachment,
            Grade = userTask.Grade,
            UserGuid = userTask.UserGuid,
            TaskGuid = userTask.TaskGuid
        };
    }
}
