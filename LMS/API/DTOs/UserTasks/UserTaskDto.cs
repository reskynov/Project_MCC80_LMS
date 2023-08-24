using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.UserTasks;

public class UserTaskDto
{
    public Guid Guid { get; set; }
    public string Attachment { get; set; }
    public int Grade { get; set; }
    public Guid UserGuid { get; set; }
    public Guid TaskGuid { get; set; }

    public static implicit operator UserTask(UserTaskDto userTaskDto)
    {
        return new UserTask
        {
            Guid = userTaskDto.Guid,
            Attachment = userTaskDto.Attachment,
            Grade = userTaskDto.Grade,
            UserGuid = userTaskDto.UserGuid,
            TaskGuid = userTaskDto.TaskGuid
        };
    }

    public static explicit operator UserTaskDto(UserTask userTask)
    {
        return new UserTaskDto
        {
            Guid = userTask.Guid,
            Attachment = userTask.Attachment,
            Grade = userTask.Grade,
            UserGuid = userTask.UserGuid,
            TaskGuid = userTask.TaskGuid
        };
    }
}
