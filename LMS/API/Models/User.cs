using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_users")]
    public class User : BaseEntity
    {
        [Column("first_name", TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [Column("last_name", TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }
        [Column("gender")]
        public GenderLevel Gender { get; set; }
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }
        [Column("email", TypeName = "nvarchar(45)")]
        public string Email { get; set; }
        [Column("phone_number", TypeName = "nvarchar(20)")]
        public string? PhoneNumber { get; set; }

        //Cardinality
        public ICollection<UserClassroom>? UserClassrooms { get; set; }
        public ICollection<Task>? Tasks { get; set; }
        public ICollection<Grade>? Grades { get; set; }
        public Account? Account { get; set; }
    }
}
