using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_grades")]
    public class Grade : BaseEntity
    {
        [Column("value")]
        public int Value { get; set; }
        [Column("user_guid")]
        public Guid UserGuid { get; set; }
        [Column("task_guid")]
        public Guid TaskGuid { get; set; }

        //Cardinality
        public User? User { get; set; }
        public Task? Task { get; set; }
    }
}
