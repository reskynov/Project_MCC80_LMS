using Client.Contracts;
using Client.Models;
using Client.ViewModels.Classrooms;
using Client.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

//[Authorize]
public class DashboardController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IClassroomRepository _classroomRepository;

    public DashboardController(IUserRepository userRepository, IClassroomRepository classroomRepository)
    {
        _userRepository = userRepository;
        _classroomRepository = classroomRepository;
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


    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await _classroomRepository.Get(id);
        var ListClassroom = new Classroom();

        if (result.Data != null)
        {
            ListClassroom = result.Data;
        }
        return View(ListClassroom);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Classroom classroom)
    {
        var result = await _classroomRepository.Put(classroom.Guid, classroom);

        if (result.Code == 200)
        {
            TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
            return RedirectToAction("Index", "Classroom");
        }
        return RedirectToAction(nameof(Edit));
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
}
