using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    [Table("tb_m_lessons")]
    public class Lesson : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(max)")]
        public string Name { get; set; }
        [Column("description", TypeName = "nvarchar(max)")]
        public string? Description { get; set; }
        [Column("subject_attachment", TypeName = "nvarchar(max)")]
        public string? SubjectAttachment { get; set; }
        [Column("is_task")]
        public bool IsTask { get; set; }
        [Column("classroom_guid")]
        public Guid ClassroomGuid { get; set; }

        //Cardinality
        public ICollection<Task>? Tasks { get; set; }
        public Classroom? Classroom { get; set; }
    }
}
