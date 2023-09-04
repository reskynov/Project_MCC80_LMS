using Client.Contracts;
using Client.ViewModels.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;

        public AccountController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Login()
        {

            if (User.Identity.IsAuthenticated)
            {
                // Pengguna sudah login, lakukan tindakan sesuai kebijakan Anda
                return RedirectToAction("Index", "Dashboard");
            }

            // Jika belum login, tampilkan halaman login
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            var result = await repository.Login(login);
            if (result.Code == 200)
            {
                HttpContext.Session.SetString("JWToken", result.Data.Token);
                return RedirectToAction("Index", "dashboard");
            }
            else
            {
                TempData["Failed"] = $"{result.Message}";
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Pengguna sudah login, lakukan tindakan sesuai kebijakan Anda
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            var result = await repository.Register(registerVM);
            if (result.Code == 200)
            {
                TempData["Success"] = "Register Success";
                return RedirectToAction("login", "account");
            }
            else
            {
                TempData["Failed"] = $"{result.Message}. Check your input data and try again";
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Clear authentication cookies
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "home");
        }

        [HttpGet("/account/forgot-password")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("/account/forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotVM)
        {
            var result = await repository.ForgotPassword(forgotVM);
            if (result.Code == 200)
            {
                TempData["Success"] = $"{result.Message}";
                return View();
            }
            else 
            {
                TempData["Failed"] = $"{result.Message}";
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }

        [HttpGet("/account/change-password")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("/account/change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changeVM)
        {
            var result = await repository.ChangePassword(changeVM);
            if (result.Code == 200)
            {
                TempData["Success"] = $"{result.Message}";
                return RedirectToAction("login", "account");
            }
            else
            {
                TempData["Failed"] = $"{result.Message}";
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }
    }
}
