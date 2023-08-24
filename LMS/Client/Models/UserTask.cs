using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    [Table("tb_tr_grades")]
    public class UserTask : BaseEntity
    {
        [Column("attachment", TypeName = "nvarchar(max)")]
        public string Attachment { get; set; }
        [Column("value")]
        public int Grade { get; set; }
        [Column("user_guid")]
        public Guid UserGuid { get; set; }
        [Column("task_guid")]
        public Guid TaskGuid { get; set; }

        //Cardinality
        public User? User { get; set; }
        public Task? Task { get; set; }
    }
}
