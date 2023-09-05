using Client.ViewModels.Lessons;

namespace Client.ViewModels.Users
{
    public class TeacherTaskVM
    {
        public string ClassroomName { get; set; }
        public Guid LessonGuid { get; set; }
        public string LessonName { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public int TotalGraded { get; set; }
        public int TotalNotGraded { get; set; }
        public int TotalNotSubmitted { get; set; }

        //public IEnumerable<TeacherTaskInClassVM> TaskInClassroom { get; set; }
    }
}
