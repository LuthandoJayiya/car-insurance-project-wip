using Microsoft.AspNetCore.Mvc;

namespace LLJ_CarInsuranceMS_ASPNET.Areas.Drivers.Controllers
{
    [Area("Drivers")]
    public class DriversController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
