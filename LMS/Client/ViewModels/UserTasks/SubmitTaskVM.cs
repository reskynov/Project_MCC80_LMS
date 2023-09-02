using Client.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.ViewModels.UserTasks;

public class SubmitTaskVM
{
    public IFormFile AttachmentFile { get; set; }
    public string Attachment { get; set; }
    public Guid UserGuid { get; set; }
    public Guid LessonGuid { get; set; }

}
