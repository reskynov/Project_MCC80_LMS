using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.UserTasks;

public class TaskByLessonDto
{
    public string Attachment { get; set; }
    public int Grade { get; set; }
    public Guid UserGuid { get; set; }
    public Guid TaskGuid { get; set; }

}
