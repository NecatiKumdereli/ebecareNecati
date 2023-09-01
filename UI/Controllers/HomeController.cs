using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MSC.Extentions.Filters;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AuthorizeFilter]
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeFilter]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Deneme(string text1, string text2)
        {
            return Ok(new { message1 = text1, message2 = text2 });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}