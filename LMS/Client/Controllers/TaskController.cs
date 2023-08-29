using Client.Contracts;
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

    public async Task<IActionResult> Index()
    {
        var userGuidClaim = User.FindFirst("Guid");

        if (userGuidClaim != null && Guid.TryParse(userGuidClaim.Value, out Guid guid))
        {
            var result = await _userRepository.GetStudentTask(guid);
            if (result != null)
            {
                var listTask = result.Data;
                return View(listTask);
            }
            else
            {
                return View(new List<StudentTaskVM>());
            }
        }
        else
        {
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
