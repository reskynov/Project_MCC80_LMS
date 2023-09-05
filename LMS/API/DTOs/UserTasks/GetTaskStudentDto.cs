namespace API.DTOs.UserTasks
{
    public class GetTaskStudentDto
    {
        public Guid UserTaskGuid { get; set; }
        public string StudentName { get; set; }
        public string? Attachment { get; set; }
        public int? Grade { get; set; }
    }
}
