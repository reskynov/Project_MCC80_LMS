using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.UserTasks
{
    public class GetTaskToGradeVM
    {
        public Guid LessonGuid { get; set; }
        public string LessonName { get; set; }
        public string StudentName { get; set; }
        public bool IsSubmitted { get; set; }
        public Guid? UserTaskGuid { get; set; }
        public string? SubmittedTask { get; set; }
        public DateTime? SubmittedTaskDate { get; set; }
        [Range(0, 100, ErrorMessage = "Value between 0 and 100")]
        public int? Grade { get; set; }
        public DateTime? DeadlineDate { get; set; }
    }
}
