using Client.Contracts;
using Client.ViewModels.CombinedViews;
using Client.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class TaskController : Controller
{
    private readonly IUserRepository _userRepository;
    public TaskController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userGuidClaim = User.FindFirst("Guid");

        if (userGuidClaim != null && Guid.TryParse(userGuidClaim.Value, out Guid guid))
        {
            var resultStudent = await _userRepository.GetStudentTask(guid);
            var resultTeacher = await _userRepository.GetTeacherTask(guid);

            if (resultStudent.Data == null && resultTeacher.Data == null)
            {
                return View("No Data");
            }

            var taskDetailView = new TaskDetailsVM
            {
                StudentTaskVMs = resultStudent.Data ?? new List<StudentTaskVM>(),
                TeacherTaskVMs = resultTeacher.Data ?? new List<TeacherTaskVM>()
            };

            return View(taskDetailView);
        }
        else
        {
            return RedirectToAction("Index", "Dashboard");
        }
    }

}
