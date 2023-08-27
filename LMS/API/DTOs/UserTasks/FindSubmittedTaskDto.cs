using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.UserTasks;

public class FindSubmittedTaskDto
{
    public Guid UserGuid { get; set; }
    public Guid LessonGuid { get; set; }

}
