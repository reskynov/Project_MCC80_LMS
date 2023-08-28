using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_user_tasks")]
    public class UserTask : BaseEntity
    {
        [Column("attachment", TypeName = "nvarchar(max)")]
        public string Attachment { get; set; }
        [Column("grade")]
        public int? Grade { get; set; }
        [Column("user_guid")]
        public Guid UserGuid { get; set; }
        [Column("task_guid")]
        public Guid TaskGuid { get; set; }

        //Cardinality
        public User? User { get; set; }
        public Task? Task { get; set; }
    }
}
