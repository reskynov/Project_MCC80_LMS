using Client.Contracts;
using Client.Models;
using Client.ViewModels.Grades;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeRepository gradeRepository;

        public GradeController(IGradeRepository gradeRepository)
        {
            this.gradeRepository = gradeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await gradeRepository.GetAll();
            var ListGrade = new List<Grade>();

            if (result.Data != null)
            {
                ListGrade = result.Data.ToList();
            }
            return View(ListGrade);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewGradeVM newGradeVM)
        {

            var result = await gradeRepository.Post(newGradeVM);
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
            var result = await gradeRepository.Delete(guid);
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
            var result = await gradeRepository.Get(id);
            var ListGrade = new Grade();

            if (result.Data != null)
            {
                ListGrade = result.Data;
            }
            return View(ListGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Grade grade)
        {
            var result = await gradeRepository.Put(grade.Guid, grade);

            if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction(nameof(Edit));
        }
    }
}
