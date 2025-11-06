using LLJ_CarInsuranceMS_ASPNET.Models;
using LLJ_CarInsuranceMS_ASPNET.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LLJ_CarInsuranceMS_ASPNET.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class CustomersController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger("CustomersController");

        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _httpClient;

        public CustomersController(IOptions<ClientSettings> ctSettings, HttpClient injectedClient)
        {
            _clientSettings = ctSettings.Value;
            _httpClient = injectedClient;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PolicyList()
        {
            logger.Info("User has accessed the Index page.");
            try
            {
                List<PolicyVM> allPolicies = new List<PolicyVM>();
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrl = baseUrl + "/api/Policies";


                HttpResponseMessage resp = await _httpClient.GetAsync(apiUrl);

                if (resp.IsSuccessStatusCode)
                {
                    logger.Info("Retrieving List of Policies");
                    var results = resp.Content.ReadAsStringAsync().Result;
                    allPolicies = JsonConvert.DeserializeObject<List<PolicyVM>>(results);
                }
                else
                {
                    logger.Warn("Failed to get list of Policies");
                }

                logger.Info("Showing List of policies");
                return View(allPolicies);
            }
            catch (Exception e)
            {
                logger.Error($"An Error Occured while trying to retrieve List of Policies: {e.Message}");
                return View();
            }
        }
    }
}
