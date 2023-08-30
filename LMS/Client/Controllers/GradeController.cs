using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class GradeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
