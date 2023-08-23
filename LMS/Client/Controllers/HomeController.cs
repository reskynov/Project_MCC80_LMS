using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("/unauthorized")]
        public IActionResult Unauthorized()
        {
            return View("401");
        }

        [AllowAnonymous]
        [HttpGet("/forbidden")]
        public IActionResult Forbidden()
        {
            return View("403");
        }

        [AllowAnonymous]
        [HttpGet("/not-found")]
        public IActionResult NotFound()
        {
            return View("404");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}