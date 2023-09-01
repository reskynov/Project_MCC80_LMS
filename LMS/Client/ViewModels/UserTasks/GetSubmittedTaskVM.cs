using Client.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.ViewModels.UserTasks;

public class GetSubmittedTaskVM
{
    public Guid UserTaskGuid { get; set; }
    [Required]
    public string Attachment { get; set; }
    public int? Grade { get; set; }
    public Guid UserGuid { get; set; }
    public Guid TaskGuid { get; set; }
    public DateTime SubmittedDate { get; set; }

}
