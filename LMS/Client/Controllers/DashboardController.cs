using Client.Contracts;
using Client.ViewModels.Classrooms;
using Client.ViewModels.CombinedViews;
using Client.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace Client.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IUserClassroomRepository _userClassroomRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly ILessonRepository _lessonRepository;
    public DashboardController(IUserRepository userRepository, IUserClassroomRepository userClassroomRepository, IClassroomRepository classroomRepository, ILessonRepository lessonRepository)
    {
        _userRepository = userRepository;
        _userClassroomRepository = userClassroomRepository;
        _classroomRepository = classroomRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<IActionResult> Index()
    {
        // Mendapatkan claim yang mengandung GUID pengguna dari token JWT
        var userGuidClaim = User.FindFirst("Guid");

        if (userGuidClaim != null && Guid.TryParse(userGuidClaim.Value, out Guid guid))
        {
            var result = await _userRepository.GetClassroomByUser(guid);
            Console.WriteLine(guid);
            if (result != null && result.Data != null)
            {
                var ListClass = result.Data;
                return View(ListClass); // Mengembalikan ListClass ke tampilan
            }
            else
            {
                // Handle kasus ketika data tidak tersedia
                return View(new List<ClassroomByUserVM>()); // Mengembalikan tampilan dengan daftar kosong
            }
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await _classroomRepository.Delete(guid);
        if (result.Status == "200")
        {
            TempData["Success"] = "Data Berhasil Dihapus";
        }
        else
        {
            TempData["Error"] = "Gagal Menghapus Data";
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Unenroll(Guid userClassroomGuid)
    {
        var result = await _userClassroomRepository.Delete(userClassroomGuid);
        if (result.Code == 200)
        {
            TempData["Success"] = "You have successfully unenrolled from the class.";
        }
        else
        {
            TempData["Failed"] = "Failed to unenroll from the class. Please try again later.";
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Lessons(Guid lessonByClassroomGuid)
    {
        var resultClassroomDetails = await _classroomRepository.Get(lessonByClassroomGuid);
        if (resultClassroomDetails is null)
        {
            return View("No Data");
        }

        var resultLesson = await _classroomRepository.GetLessonByClassroom(lessonByClassroomGuid);

        var classroomDescription = resultClassroomDetails.Data;

        // Memeriksa apakah ada data lesson atau tidak
        if (resultLesson != null && resultLesson.Data != null && resultLesson.Data.Any())
        {
            var listLesson = resultLesson.Data;

            var classroomDetailView = new ClassroomDetailsVM
            {
                ClassroomModel = classroomDescription,
                ClassroomLessonViewModels = listLesson
            };

            return View(classroomDetailView);
        }
        else
        {
            // Kasus ketika tidak ada data lesson
            var classroomDetailView = new ClassroomDetailsVM
            {
                ClassroomModel = classroomDescription,
                ClassroomLessonViewModels = new List<ClassroomLessonVM>() // Koleksi kosong
            };

            return View(classroomDetailView);
        }
    }

    public async Task<IActionResult> LessonDetail(Guid lessonGuid)
    {
        var result = await _lessonRepository.Get(lessonGuid);
        if (result != null)
        {
            var lessonDetail = result.Data;
            return View(lessonDetail);
        }

        return View(null);
    }

    public async Task<IActionResult> GetPeople(Guid classroomGuid)
    {
        var result = await _classroomRepository.GetPeople(classroomGuid);
        if (result != null)
        {
            var listPeople = result.Data;
            return View(listPeople);
        }
        return View(null);
    }
}
