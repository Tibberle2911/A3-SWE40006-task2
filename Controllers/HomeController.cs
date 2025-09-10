using A3_SWE40006_C_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace A3_SWE40006_C_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // -----------------------------
        // Main Pages
        // -----------------------------

        // GET: /
        public IActionResult Index()
        {
            ViewData["Message"] = "Welcome to A3_SWE40006_C_ Web Application!";
            return View();
        }

        // POST: Calculator Function
        [HttpPost]
        public IActionResult Index(double number1, double number2, string operation)
        {
            double result = 0;

            switch (operation)
            {
                case "Add":
                    result = number1 + number2;
                    break;
                case "Subtract":
                    result = number1 - number2;
                    break;
                case "Multiply":
                    result = number1 * number2;
                    break;
                case "Divide":
                    result = number2 != 0 ? number1 / number2 : double.NaN;
                    break;
                default:
                    ViewData["Result"] = "Invalid Operation";
                    return View();
            }

            ViewData["Message"] = "Welcome to A3_SWE40006_C_ Web Application!";
            ViewData["Result"] = result;
            return View();
        }

        // GET: /Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: /Home/About
        public IActionResult About()
        {
            ViewData["Message"] = "This is the About page. You can describe your app here.";
            return View();
        }

        // -----------------------------
        // Error Handling
        // -----------------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            _logger.LogError("Error encountered with RequestId: {RequestId}", errorModel.RequestId);

            return View(errorModel);
        }
    }
}
