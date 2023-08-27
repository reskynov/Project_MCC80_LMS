using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.UserTasks;

public class SubmitTaskDto
{
    public string Attachment { get; set; }
    public Guid UserGuid { get; set; }
    public Guid LessonGuid { get; set; }

}
