namespace API.DTOs.Lessons
{
    public class StudentTaskInClassDto
    {
        public Guid LessonGuid { get; set; }
        public string LessonName { get; set; }
        public DateTime? Deadline { get; set; }
        public bool? IsSubmitted { get; set; }
        public int? Grade { get; set; }
        public Guid? TaskGuid { get; set; }
    }
}
