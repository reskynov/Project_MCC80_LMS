using API.Models;
using Task = API.Models.Task;

namespace API.DTOs.Tasks
{
    public class TaskDto
    {
        public Guid Guid { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public Guid UserGuid { get; set; }
        public Guid LessonGuid { get; set; }

        public static implicit operator Task(TaskDto taskDto)
        {
            return new Task
            {
                Guid = taskDto.Guid,
                DeadlineDate = taskDto.DeadlineDate,
                UserGuid = taskDto.UserGuid,
                LessonGuid = taskDto.LessonGuid,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator TaskDto(Task task)
        {
            return new TaskDto
            {
                Guid = task.Guid,
                DeadlineDate = task.DeadlineDate,
                UserGuid = task.UserGuid,
                LessonGuid = task.LessonGuid
            };
        }
    }
}
