namespace Client.ViewModels.Lessons
{
    public class TeacherTaskInClassVM
    {
        public Guid LessonGuid { get; set; }
        public string LessonName { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public int TotalGraded { get; set; }
        public int TotalNotGraded { get; set; }
        public int TotalNotSubmitted { get; set; }
    }
}
