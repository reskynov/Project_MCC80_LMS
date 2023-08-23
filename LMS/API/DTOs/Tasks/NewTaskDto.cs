using API.DTOs.Roles;
using API.Models;
using Task = API.Models.Task;

namespace API.DTOs.Tasks
{
    public class NewTaskDto
    {
        public string Attachment { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public Guid UserGuid { get; set; }
        public Guid LessonGuid { get; set; }

        public static implicit operator Task(NewTaskDto newTaskDto)
        {
            return new Task
            {
                Guid = new Guid(),
                Attachment = newTaskDto.Attachment,
                DeadlineDate = newTaskDto.DeadlineDate,
                UserGuid = newTaskDto.UserGuid,
                LessonGuid = newTaskDto.LessonGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewTaskDto(Task task)
        {
            return new NewTaskDto
            {
                Attachment = task.Attachment,
                DeadlineDate = task.DeadlineDate,
                UserGuid = task.UserGuid,
                LessonGuid = task.LessonGuid
            };
        }
    }
}
