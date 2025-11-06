using Microsoft.AspNetCore.Mvc;

namespace LLJ_CarInsuranceMS_ASPNET.Areas.ClaimSurveyor.Controllers
{
    [Area("ClaimSurveyor")]
    public class SurveyorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
