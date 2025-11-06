using LLJ_CarInsuranceMS_ASPNET.Models;
using LLJ_CarInsuranceMS_ASPNET.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LLJ_CarInsuranceMS_ASPNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CrudCustomersController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger("CrudCustomersController");

        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _httpClient;
        public CrudCustomersController(IOptions<ClientSettings> ctSettings, HttpClient injectedClient)
        {
            _clientSettings = ctSettings.Value;
            _httpClient = injectedClient;
        }
        public async Task<IActionResult> Index()
        {

            logger.Info("User has accessed the Index page.");
            try
            {
                List<PotentialCustomer> allCustomers = new List<PotentialCustomer>();
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrl = baseUrl + "/api/Customers";

                string? _userLoginToken = TempData["UserToken"]?.ToString();
                string? _tokenValidation = TempData["UserTokenValidTo"]?.ToString();
                TempData.Keep();
                _httpClient.AddBearerToken(_userLoginToken);

                HttpResponseMessage resp = await _httpClient.GetAsync(apiUrl);

                if (resp.IsSuccessStatusCode)
                {
                    logger.Info("Retrieving List of Customers");
                    var results = resp.Content.ReadAsStringAsync().Result;
                    allCustomers = JsonConvert.DeserializeObject<List<PotentialCustomer>>(results);
                }
                else
                {
                    logger.Warn("Failed to get list of Customers");
                }

                logger.Info("Showing List of Customers");
                return View(allCustomers);
            }
            catch (Exception e)
            {
                logger.Error($"An Error Occured while trying to retrieve List of Customers: {e.Message}");
                return View();
            }
        }
    }
}
