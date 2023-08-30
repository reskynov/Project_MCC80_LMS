namespace Client.ViewModels.Lessons
{
    public class TeacherTaskInClassVM
    {
        public Guid LessonGuid { get; set; }
        public string LessonName { get; set; }
        public DateTime? DeadlineDate { get; set; }
    }
}
