using Client.Models;

namespace Client.ViewModels.UserTasks;

public class NewUserTaskVM
{
    public string Attachment { get; set; }
    public int Grade { get; set; }
    public Guid UserGuid { get; set; }
    public Guid TaskGuid { get; set; }

    public static implicit operator UserTask(NewUserTaskVM newUserTaskDto)
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

    public static explicit operator NewUserTaskVM(UserTask userTask)
    {
        return new NewUserTaskVM
        {
            Attachment = userTask.Attachment,
            Grade = userTask.Grade,
            UserGuid = userTask.UserGuid,
            TaskGuid = userTask.TaskGuid
        };
    }
}
