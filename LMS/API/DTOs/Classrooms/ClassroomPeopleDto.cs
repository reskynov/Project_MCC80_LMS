using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Classrooms
{
    public class ClassroomPeopleDto
    {
        public Guid Guid { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }

    }
}
