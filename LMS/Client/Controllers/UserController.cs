using Client.Contracts;
using Client.Models;
using Client.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await userRepository.GetAll();
            var ListTeacher = new List<User>();

            if (result.Data != null)
            {
                ListTeacher = result.Data.ToList();
            }
            return View(ListTeacher);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewUserVM newUserVM)
        {

            var result = await userRepository.Post(newUserVM);
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
            var result = await userRepository.Delete(guid);
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
            var result = await userRepository.Get(id);
            var ListTeacher = new User();

            if (result.Data != null)
            {
                ListTeacher = result.Data;
            }
            return View(ListTeacher);
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            var result = await userRepository.Put(user.Guid, user);

            if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction(nameof(Edit));
        }
    }
}
