using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_tasks")]
    public class Task : BaseEntity
    {
        [Column("attachment", TypeName = "nvarchar(max)")]
        public string Attachment { get; set; }
        [Column("user_guid")]
        public Guid UserGuid { get; set; }
        [Column("lesson_guid")]
        public Guid LessonGuid { get; set; }

        //Cardinality
        public ICollection<Grade>? Grades { get; set; }
        public Lesson? Lesson { get; set; }
        public User? User { get; set; }
    }
}
