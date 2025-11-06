using LLJ_CarInsuranceMS_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LLJ_CarInsuranceMS_ASPNET.Controllers
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
            _logger.LogInformation("Index");
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Accessing Privacy");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            _logger.LogInformation("Accessing Contact");
            return View();
        }
    }
}
