using Client.Contracts;
using Client.Models;
using Client.ViewModels.Accounts;
using Client.ViewModels.Classrooms;
using Client.ViewModels.CombinedViews;
using Client.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using NuGet.Protocol.Core.Types;
using System.Diagnostics.Contracts;

namespace Client.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IUserClassroomRepository _userClassroomRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IUserTaskRepository _userTaskRepository;
    public DashboardController(IUserRepository userRepository, IUserClassroomRepository userClassroomRepository, IClassroomRepository classroomRepository, ILessonRepository lessonRepository, IUserTaskRepository userTaskRepository)
    {
        _userRepository = userRepository;
        _userClassroomRepository = userClassroomRepository;
        _classroomRepository = classroomRepository;
        _lessonRepository = lessonRepository;
        _userTaskRepository = userTaskRepository;
    }

    public async Task<IActionResult> Index()
    {
        var userGuidClaim = User.FindFirst("Guid");
        if (userGuidClaim != null && Guid.TryParse(userGuidClaim.Value, out Guid guid))
        {
            var result = await _userRepository.DashboardStudent(guid);
            if (result != null && result.Data != null)
            {
                var dataDashboard = result.Data;
                return View(dataDashboard); // Mengembalikan ListClass ke tampilan
            }
            else
            {
                return View(null);
            }
        } else
        {
            return RedirectToAction("Index", "Home");
        }
    }

    public async Task<IActionResult> Classroom()
    {
        // Mendapatkan claim yang mengandung GUID pengguna dari token JWT
        var userGuidClaim = User.FindFirst("Guid");

        if (userGuidClaim != null && Guid.TryParse(userGuidClaim.Value, out Guid guid))
        {
            var result = await _userRepository.GetClassroomByUser(guid);
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
            TempData["Success"] = "Data has been successfully deleted";
        }
        else
        {
            TempData["Failed"] = "Failed to delete Data";
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
        var classroomDescription = resultClassroomDetails.Data;

        var resultLesson = await _classroomRepository.GetLessonByClassroom(lessonByClassroomGuid);

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
        var userGuidClaim = User.FindFirst("Guid");

        if (userGuidClaim != null && Guid.TryParse(userGuidClaim.Value, out Guid guid))
        {

           var resultSubmittedTask = await _userTaskRepository.GetSubmittedTask(guid, lessonGuid);
           if (resultSubmittedTask is null)
           {
               return View("LessonDetail","Dashboard");
           }
           var dataSubmittedTask = resultSubmittedTask.Data;
         
           var resultLessonDetail = await _lessonRepository.Get(lessonGuid);
         
           if (resultLessonDetail != null && resultLessonDetail.Data != null)
           {
               var lessonDetail = resultLessonDetail.Data;
         
               var lessonDetailView = new LessonDetailsVM
               {
                   LessonModel = lessonDetail,
                   GetSubmittedTaskVM = dataSubmittedTask
               };
         
               return View(lessonDetailView);
           }
           else
           {
               var lessonDetailView = new LessonDetailsVM
               {
                   LessonModel = new Lesson(),
                   GetSubmittedTaskVM = dataSubmittedTask
               };
               return View(lessonDetailView);
           }
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }


        //if (result != null)
        //{
        //    var lessonDetail = result.Data;
        //    return View(lessonDetail);
        //}

        //return View(null);
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

    [HttpGet("/dashboard/profile")]
    public async Task<ActionResult> Profile()
    {
        var userGuidClaim = User.FindFirst("Guid");
        if (userGuidClaim != null && Guid.TryParse(userGuidClaim.Value, out Guid guid))
        {
            var result = await _userRepository.GetProfile(guid);
            if (result != null)
            {
                var userDetail = result.Data;
                return View(userDetail);
            }
        }
        return View(null);
    }

    [HttpPost("/dashboard/update-profile")]
    public async Task<IActionResult> UpdateProfile(UserVM user)
    {
        var userGuidClaim = User.FindFirst("Guid");
        if (userGuidClaim != null && Guid.TryParse(userGuidClaim.Value, out Guid guid))
        {
            user.Guid = guid;
            var result = await _userRepository.Put(user.Guid, user);
            if (result.Code == 200)
            {
                TempData["Success"] = $"{result.Message}!";
                return RedirectToAction("Profile","Dashboard");
            }
            else
            {
                TempData["Failed"] = $"{result.Message}";
                ModelState.AddModelError(string.Empty, result.Message);
                return View(nameof(Profile));
            }
        }
        return RedirectToAction("Profile", "Dashboard");
    }

    [HttpPost("/dashboard/profile-change-password")]
    public async Task<IActionResult> ProfileChangePassword(ProfileChangePasswordVM profileChangePasswordVM)
    {
        var userGuidClaim = User.FindFirst("Guid");
        if (userGuidClaim != null && Guid.TryParse(userGuidClaim.Value, out Guid guid))
        {
            profileChangePasswordVM.GuidAccount = guid;
            var result = await _userRepository.ProfileChangePassword(profileChangePasswordVM);
            if (result.Code == 200)
            {
                TempData["Success"] = $"{result.Message}!";
                return RedirectToAction("Profile", "Dashboard");
            }
            else
            {
                TempData["Failed"] = $"{result.Message}";
                ModelState.AddModelError(string.Empty, result.Message);
                return View(nameof(Profile));
            }
        }
        return RedirectToAction("Profile", "Dashboard");
    }
}
