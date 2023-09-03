using Client.Models;
using Client.ViewModels.Classrooms;
using Client.ViewModels.Lessons;

namespace Client.ViewModels.CombinedViews;

public class ClassroomDetailsVM
{
    public IEnumerable<ClassroomLessonVM> ClassroomLessonViewModels { get; set; }
    public Classroom ClassroomModel { get; set; }
    public Guid UserClassroomGuid { get; set; }
    public NewLessonTaskVM NewLessonTaskVM { get; set;}
    public UpdateLessonTaskVM UpdateLessonTaskVM { get; set;}
}