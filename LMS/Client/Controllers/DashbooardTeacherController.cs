using Client.Contracts;
using Client.Models;
using Client.ViewModels.Classrooms;
using Client.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

//[Authorize]
public class DashboardTeacherController : Controller
{

    private readonly IClassroomRepository _classroomRepository;

    public DashboardTeacherController(IClassroomRepository classroomRepository)
    {
        _classroomRepository = classroomRepository;
    }

    public async Task<IActionResult> Index()
    {
        // Mendapatkan claim yang mengandung GUID pengguna dari token JWT
        var userGuidClaim = User.FindFirst("Guid");

        if (userGuidClaim != null && Guid.TryParse(userGuidClaim.Value, out Guid guid))
        {
            var result = await _classroomRepository.GetClassroomPeople(guid);
            Console.WriteLine(guid);
            if (result != null && result.Data != null)
            {
                var ListClass = result.Data;
                return View(ListClass); // Mengembalikan ListClass ke tampilan
            }
            else
            {
                // Handle kasus ketika data tidak tersedia
                return View(new List<ClassroomPeopleVM>()); // Mengembalikan tampilan dengan daftar kosong
            }
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }

    //public async Task<IActionResult> IndexClassroomPeople()
    //{
    //    // Mendapatkan claim yang mengandung GUID pengguna dari token JWT
    //    var userGuid = User.FindFirst("Guid");

    //    if (userGuid != null && Guid.TryParse(userGuid.Value, out Guid guid))
    //    {
    //        var result = await _classroomRepository.GetClassroomPeople(guid);
    //        Console.WriteLine(guid);
    //        if (result != null && result.Data != null)
    //        {
    //            var ListPeople = result.Data;
    //            return View(ListPeople); // Mengembalikan ListClass ke tampilan
    //        }
    //        else
    //        {
    //            // Handle kasus ketika data tidak tersedia
    //            return View(new List<ClassroomPeopleVM>()); // Mengembalikan tampilan dengan daftar kosong
    //        }
    //    }
    //    else
    //    {
    //        return RedirectToAction("Index", "Home");
    //    }
    //}
}
