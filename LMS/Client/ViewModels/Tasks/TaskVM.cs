using Client.Models;
using Task = Client.Models.Task;

namespace Client.ViewModels.Tasks
{
    public class TaskVM
    {
        public Guid Guid { get; set; }
        public string Attachment { get; set; }
        public Guid UserGuid { get; set; }
        public Guid LessonGuid { get; set; }

        public static implicit operator Task(TaskVM taskDto)
        {
            return new Task
            {
                Guid = taskDto.Guid,
                Attachment = taskDto.Attachment,
                UserGuid = taskDto.UserGuid,
                LessonGuid = taskDto.LessonGuid,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator TaskVM(Task task)
        {
            return new TaskVM
            {
                Guid = task.Guid,
                Attachment = task.Attachment,
                UserGuid = task.UserGuid,
                LessonGuid = task.LessonGuid
            };
        }
    }
}
