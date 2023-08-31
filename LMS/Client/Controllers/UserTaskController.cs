using Client.Contracts;
using Client.Models;
using Client.ViewModels.UserTasks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class UserTaskController : Controller
    {
        private readonly IUserTaskRepository _userTaskRepository;

        public UserTaskController(IUserTaskRepository userTaskRepository)
        {
            _userTaskRepository = userTaskRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userTaskRepository.GetAll();
            var ListUserTask = new List<UserTask>();

            if (result.Data != null && result.Data.Any())
            {
                ListUserTask = result.Data.ToList();
            }
            else
            {
                ListUserTask = new List<UserTask>();
            }
            return View(ListUserTask);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewUserTaskVM newUserTaskVM)
        {

            var result = await _userTaskRepository.Post(newUserTaskVM);
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
            var result = await _userTaskRepository.Delete(guid);
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
            var result = await _userTaskRepository.Get(id);
            var ListUserTask = new UserTask();

            if (result.Data != null)
            {
                ListUserTask = result.Data;
            }
            return View(ListUserTask);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserTask UserTask)
        {
            var result = await _userTaskRepository.Put(UserTask.Guid, UserTask);

            if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction(nameof(Edit));
        }
    }
}
