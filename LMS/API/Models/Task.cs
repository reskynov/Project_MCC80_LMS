﻿using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_tasks")]
    public class Task : BaseEntity
    {
        [Column("deadline_date")]
        public DateTime? DeadlineDate { get; set; }
        [Column("lesson_guid")]
        public Guid LessonGuid { get; set; }

        //Cardinality
        public ICollection<UserTask>? Grades { get; set; }
        public Lesson? Lesson { get; set; }
    }
}
