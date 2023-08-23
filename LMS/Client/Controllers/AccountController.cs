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
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            var result = await repository.Register(registerVM);
            if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Added! - {result.Message}!";
                return RedirectToAction(nameof(Login));
            }
            else
            {
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
                TempData["Success"] = $"OTP has been sent - {result.Message}!";
                return RedirectToAction(nameof(Login));
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }
    }
}
