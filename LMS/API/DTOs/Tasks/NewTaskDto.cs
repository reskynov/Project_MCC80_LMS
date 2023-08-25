using API.DTOs.Roles;
using API.Models;
using Task = API.Models.Task;

namespace API.DTOs.Tasks
{
    public class NewTaskDto
    {
        public DateTime? DeadlineDate { get; set; }
        public Guid LessonGuid { get; set; }

        public static implicit operator Task(NewTaskDto newTaskDto)
        {
            return new Task
            {
                Guid = new Guid(),
                DeadlineDate = newTaskDto.DeadlineDate,
                LessonGuid = newTaskDto.LessonGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewTaskDto(Task task)
        {
            return new NewTaskDto
            {
                DeadlineDate = task.DeadlineDate,
                LessonGuid = task.LessonGuid
            };
        }
    }
}
