namespace API.DTOs.Lessons
{
    public class TeacherTaskInClassDto
    {
        public Guid LessonGuid { get; set; }
        public string LessonName { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public int TotalGraded { get; set; }
        public int TotalNotGraded { get; set; }
        public int TotalNotSubmitted { get; set; }
    }
}
