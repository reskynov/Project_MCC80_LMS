using Client.ViewModels.Lessons;

namespace Client.ViewModels.Users
{
    public class TeacherTaskVM
    {
        public string ClassroomName { get; set; }

        public IEnumerable<TeacherTaskInClassVM> TaskInClassroom { get; set; }
    }
}
