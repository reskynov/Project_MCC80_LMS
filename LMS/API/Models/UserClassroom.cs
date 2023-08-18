using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_user_classrooms")]
    public class UserClassroom : BaseEntity
    {
        [Column("user_guid")]
        public Guid UserGuid { get; set; }
        [Column("classroom_guid")]
        public Guid ClassroomGuid { get; set; }

        //Cardinality
        public User? User { get; set; }
        public Classroom? Classroom { get; set; }
    }
}
