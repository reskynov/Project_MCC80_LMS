using Client.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.ViewModels.UserTasks;

public class UserTaskVM
{
    public Guid Guid { get; set; }
    public string Attachment { get; set; }
    public int Grade { get; set; }
    public Guid UserGuid { get; set; }
    public Guid TaskGuid { get; set; }

    public static implicit operator UserTask(UserTaskVM userTaskDto)
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

    public static explicit operator UserTaskVM(UserTask userTask)
    {
        return new UserTaskVM
        {
            Guid = userTask.Guid,
            Attachment = userTask.Attachment,
            Grade = userTask.Grade,
            UserGuid = userTask.UserGuid,
            TaskGuid = userTask.TaskGuid
        };
    }
}
