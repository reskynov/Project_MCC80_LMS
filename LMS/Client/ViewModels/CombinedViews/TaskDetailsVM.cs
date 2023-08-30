using Client.ViewModels.Lessons;
using Client.ViewModels.Users;

namespace Client.ViewModels.CombinedViews;

public class TaskDetailsVM
{
    public IEnumerable<StudentTaskVM> StudentTaskVMs { get; set; }
    public IEnumerable<TeacherTaskVM> TeacherTaskVMs { get; set ; }
}