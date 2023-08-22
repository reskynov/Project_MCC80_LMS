using Client.Contracts;
using Client.Models;
using Client.ViewModels.Lessons;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILessonRepository lessonRepository;

        public LessonController(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await lessonRepository.GetAll();
            var ListLesson = new List<Lesson>();

            if (result.Data != null)
            {
                ListLesson = result.Data.ToList();
            }
            return View(ListLesson);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewLessonVM newLessonVM)
        {

            var result = await lessonRepository.Post(newLessonVM);
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
            var result = await lessonRepository.Delete(guid);
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
            var result = await lessonRepository.Get(id);
            var ListLesson = new Lesson();

            if (result.Data != null)
            {
                ListLesson = result.Data;
            }
            return View(ListLesson);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Lesson lesson)
        {
            var result = await lessonRepository.Put(lesson.Guid, lesson);

            if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Index", "Lesson");
            }
            return RedirectToAction(nameof(Edit));
        }
    }
}
