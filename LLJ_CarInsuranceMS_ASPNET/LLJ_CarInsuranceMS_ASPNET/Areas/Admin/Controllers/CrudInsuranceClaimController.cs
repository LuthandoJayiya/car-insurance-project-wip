using LLJ_CarInsuranceMS_ASPNET.Models;
using LLJ_CarInsuranceMS_ASPNET.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LLJ_CarInsuranceMS_ASPNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CrudInsuranceClaimController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger("CrudInsuranceClaimController");

        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _httpClient;
        public CrudInsuranceClaimController(IOptions<ClientSettings> ctSettings, HttpClient injectedClient)
        {
            _clientSettings = ctSettings.Value;
            _httpClient = injectedClient;
        }
        public async Task<IActionResult> Index()
        {

            logger.Info("User has accessed the Index page.");
            try
            {
                List<InsuranceClaim> allClaims = new List<InsuranceClaim>();
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrl = baseUrl + "/api/InsuranceClaims";

                string? _userLoginToken = TempData["UserToken"]?.ToString();
                string? _tokenValidation = TempData["UserTokenValidTo"]?.ToString();
                TempData.Keep();
                _httpClient.AddBearerToken(_userLoginToken);

                HttpResponseMessage resp = await _httpClient.GetAsync(apiUrl);

                if (resp.IsSuccessStatusCode)
                {
                    logger.Info("Retrieving List of Claims");
                    var results = resp.Content.ReadAsStringAsync().Result;
                    allClaims = JsonConvert.DeserializeObject<List<InsuranceClaim>>(results);
                }
                else
                {
                    logger.Warn("Failed to get list of Claims");
                }

                logger.Info("Showing List of Claims");
                return View(allClaims);
            }
            catch (Exception e)
            {
                logger.Error($"An Error Occured while trying to retrieve List of Claims: {e.Message}");
                return View();
            }
        }
    }
}
