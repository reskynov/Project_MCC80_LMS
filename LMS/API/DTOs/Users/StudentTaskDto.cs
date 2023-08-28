namespace API.DTOs.Users
{
    public class StudentTaskDto
    {
        public string ClassroomName { get; set; }
        public Guid LessonGuid { get; set; }
        public string LessonName { get; set; }
        public DateTime? Deadline { get; set; }
        public bool? IsSubmitted { get; set; }
        public int? Grade { get; set; }
        public Guid TaskGuid { get; set; }
    }
}
