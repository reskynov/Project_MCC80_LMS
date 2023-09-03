using Client.Models;
using Client.ViewModels.Classrooms;
using Client.ViewModels.Lessons;
using Client.ViewModels.UserTasks;

namespace Client.ViewModels.CombinedViews;

public class LessonWithTaskVM
{
    public LessonDetailVM LessonDetailVM { get; set; }
    public GetSubmittedTaskVM GetSubmittedTaskVM { get; set; }
    public SubmitTaskVM SubmitTaskVM { get; set; }
}