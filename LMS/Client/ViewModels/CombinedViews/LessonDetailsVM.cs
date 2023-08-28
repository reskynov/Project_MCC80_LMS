using Client.Models;
using Client.ViewModels.Classrooms;
using Client.ViewModels.UserTasks;

namespace Client.ViewModels.CombinedViews;

public class LessonDetailsVM
{
    public Lesson LessonModel { get; set; }
    public GetSubmittedTaskVM GetSubmittedTaskVM { get; set; }
}