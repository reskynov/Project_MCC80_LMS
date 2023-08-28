using Client.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.ViewModels.UserTasks;

public class GetSubmittedTaskVM
{
    public Guid UserTaskGuid { get; set; }
    public string Attachment { get; set; }
    public int? Grade { get; set; }
    public Guid UserGuid { get; set; }
    public Guid TaskGuid { get; set; }

}
