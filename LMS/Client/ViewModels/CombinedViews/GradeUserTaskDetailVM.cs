using Client.Models;
using Client.ViewModels.Classrooms;
using Client.ViewModels.UserTasks;

namespace Client.ViewModels.CombinedViews;

public class GradeUserTaskDetailVM
{
    public Lesson LessonModel { get; set; }
    public IEnumerable<GetTaskToGradeVM> GetTaskToGradeVM { get; set; }
}