using LLJ_CarInsuranceMS_ASPNET.Models;
using LLJ_CarInsuranceMS_ASPNET.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LLJ_CarInsuranceMS_ASPNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RepairShopsController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger("RepairShopsController");

        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _httpClient;

        public RepairShopsController(IOptions<ClientSettings> ctSettings, HttpClient injectedClient)
        {
            _clientSettings = ctSettings.Value;
            _httpClient = injectedClient;
        }
        public async Task<IActionResult> Index()
        {

            logger.Info("User has accessed the Index page.");
            try
            {
                List<RepairShop> allShops = new List<RepairShop>();
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrl = baseUrl + "/api/RepairShops";

                HttpResponseMessage resp = await _httpClient.GetAsync(apiUrl);

                if (resp.IsSuccessStatusCode)
                {
                    logger.Info("Retrieving List of Repair Shops");
                    var results = resp.Content.ReadAsStringAsync().Result;
                    allShops = JsonConvert.DeserializeObject<List<RepairShop>>(results);
                }
                else
                {
                    logger.Warn("Failed to get list of Repair Shops");
                }

                logger.Info("Showing List of Repair Shops");
                return View(allShops);
            }
            catch (Exception e)
            {
                logger.Error($"An Error Occured while trying to retrieve List of Repair Shops: {e.Message}");
                return View();
            }
        }
    }
}
