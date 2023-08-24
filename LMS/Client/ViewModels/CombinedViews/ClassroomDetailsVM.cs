using Client.Models;
using Client.ViewModels.Classrooms;

namespace Client.ViewModels.CombinedViews;

public class ClassroomDetailsVM
{
    public IEnumerable<ClassroomLessonVM> ClassroomLessonViewModels { get; set; }
    public Classroom ClassroomModel { get; set; }
    public Guid UserClassroomGuid { get; set; }
}
