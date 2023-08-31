using Client.Contracts;
using Client.Models;
using Client.ViewModels.Classrooms;
using Client.ViewModels.CombinedViews;
using Client.ViewModels.Lessons;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly IClassroomRepository classroomRepository;
        private readonly IUserTaskRepository userTaskRepository;
        private readonly ILessonRepository lessonRepository;

        public ClassroomController(IClassroomRepository classroomRepository, IUserTaskRepository userTaskRepository, ILessonRepository lessonRepository)
        {
            this.classroomRepository = classroomRepository;
            this.userTaskRepository = userTaskRepository;
            this.lessonRepository = lessonRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await classroomRepository.GetAll();
            var ListClassroom = new List<Classroom>();

            if (result.Data != null)
            {
                ListClassroom = result.Data.ToList();
            }
            return View(ListClassroom);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewClassroomVM newClassroomVM)
        {

            var result = await classroomRepository.Post(newClassroomVM);
            if (result.Status == "200")
            {
                TempData["Success"] = "Data berhasil masuk";
                return RedirectToAction(nameof(Index));
            }
            else if (result.Status == "409")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await classroomRepository.Delete(guid);
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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await classroomRepository.Get(id);
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
            var result = await classroomRepository.Put(classroom.Guid, classroom);

            if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Index", "Classroom");
            }
            return RedirectToAction(nameof(Edit));
        }

        public async Task<IActionResult> LessonDetail(Guid lessonGuid)
        {
            var userGuidClaim = User.FindFirst("Guid");

            if (userGuidClaim != null && Guid.TryParse(userGuidClaim.Value, out Guid guid))
            {
                var resultSubmittedTask = await userTaskRepository.GetSubmittedTask(guid, lessonGuid);
                if (resultSubmittedTask is null)
                {
                    return View("LessonDetail", "Classroom");
                }
                var dataSubmittedTask = resultSubmittedTask.Data;

                var resultLessonDetail = await lessonRepository.GetLessonDetailByGuid(lessonGuid);
                if (resultLessonDetail != null && resultLessonDetail.Data != null)
                {
                    var lessonDetail = resultLessonDetail.Data;

                    var lessonDetailView = new LessonWithTaskVM
                    {
                        LessonDetailVM = lessonDetail,
                        GetSubmittedTaskVM = dataSubmittedTask
                    };

                    return View(lessonDetailView);
                }
                else
                {
                    var lessonDetailView = new LessonWithTaskVM
                    {
                        LessonDetailVM = new LessonDetailVM(),
                        GetSubmittedTaskVM = dataSubmittedTask
                    };

                    return View(lessonDetailView);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
