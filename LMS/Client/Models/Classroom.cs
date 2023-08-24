using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    [Table("tb_m_classrooms")]
    public class Classroom : BaseEntity
    {
        [Column("code", TypeName = "nvarchar(10)")]
        public string Code { get; set; }
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column("description", TypeName = "nvarchar(max)")]
        public string? Description { get; set; }
        public Guid TeacherGuid { get; set; }
        [Column("expired_date")]
        public DateTime ExpiredDate { get; set; }
        [Column("teacher_guid")]
        public Guid TeacherGuid { get; set; }

        //Cardinality
        public ICollection<UserClassroom>? UserClassrooms { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
    }
}
