using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(string details)
        {
            ViewBag.details = details;
            return View();
        }
    }
}
