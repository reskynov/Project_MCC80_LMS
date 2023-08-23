using Client.Contracts;
using Client.Models;
using Client.ViewModels.Classrooms;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly IClassroomRepository classroomRepository;

        public ClassroomController(IClassroomRepository classroomRepository)
        {
            this.classroomRepository = classroomRepository;
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
    }
}
