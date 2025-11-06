using LLJ_CarInsuranceMS_ASPNET.Models;
using LLJ_CarInsuranceMS_ASPNET.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LLJ_CarInsuranceMS_ASPNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CrudPolicyApplicationController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger("CrudPolicyApplicationController");

        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _httpClient;

        public CrudPolicyApplicationController(IOptions<ClientSettings> ctSettings, HttpClient injectedClient)
        {
            _clientSettings = ctSettings.Value;
            _httpClient = injectedClient;
        }
        public async Task<IActionResult> Index()
        {
            logger.Info("User has accessed the Index page.");
            try
            {
                List<PolicyApplication> allApplications = new List<PolicyApplication>();
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrl = baseUrl + "/api/PolicyApplication";

                string? _userLoginToken = TempData["UserToken"]?.ToString();
                string? _tokenValidation = TempData["UserTokenValidTo"]?.ToString();
                TempData.Keep();
                _httpClient.AddBearerToken(_userLoginToken);

                HttpResponseMessage resp = await _httpClient.GetAsync(apiUrl);

                if (resp.IsSuccessStatusCode)
                {
                    logger.Info("Retrieving List of Policy Applications");
                    var results = resp.Content.ReadAsStringAsync().Result;
                    allApplications = JsonConvert.DeserializeObject<List<PolicyApplication>>(results);
                }
                else
                {
                    logger.Warn("Failed to get list of Applications");
                }

                logger.Info("Showing List of Applications");
                return View(allApplications);
            }
            catch (Exception e)
            {
                logger.Error($"An Error Occured while trying to retrieve List of Applications: {e.Message}");
                return View();
            }
        }
    }
}
