using Client.ViewModels.Roles;
using Client.Models;
using Task = Client.Models.Task;

namespace Client.ViewModels.Tasks
{
    public class NewTaskVM
    {
        public string Attachment { get; set; }
        public Guid UserGuid { get; set; }
        public Guid LessonGuid { get; set; }

        public static implicit operator Task(NewTaskVM newTaskDto)
        {
            return new Task
            {
                Guid = new Guid(),
                Attachment = newTaskDto.Attachment,
                UserGuid = newTaskDto.UserGuid,
                LessonGuid = newTaskDto.LessonGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewTaskVM(Task task)
        {
            return new NewTaskVM
            {
                Attachment = task.Attachment,
                UserGuid = task.UserGuid,
                LessonGuid = task.LessonGuid
            };
        }
    }
}
