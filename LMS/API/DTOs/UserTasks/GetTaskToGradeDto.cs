namespace API.DTOs.UserTasks
{
    public class GetTaskToGradeDto
    {
        public Guid LessonGuid { get; set; }
        public string LessonName { get; set; }
        public string StudentName { get; set; }
        public bool IsSubmitted { get; set; }
        public Guid? UserTaskGuid { get; set; }
        public string? SubmittedTask { get; set; }
        public int? Grade { get; set; }
    }
}
