using LLJ_CarInsuranceMS_ASPNET.Models;
using LLJ_CarInsuranceMS_ASPNET.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace LLJ_CarInsuranceMS_ASPNET.Controllers
{
    public class PoliciesController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger("PoliciesController");

        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _httpClient;

        public PoliciesController(IOptions<ClientSettings> ctSettings, HttpClient injectedClient)
        {
            _clientSettings = ctSettings.Value;
            _httpClient = injectedClient;
        }

        public async Task<IActionResult> Index()
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

        public async Task<IActionResult> Details(int id)
        {
            logger.Info("User has accessed the Details Page.");
            try
            {
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrl = baseUrl + $"/api/Policies/{id}";

                PolicyVM policyById = new PolicyVM();

                HttpResponseMessage resp = await _httpClient.GetAsync(apiUrl);
                if (resp.IsSuccessStatusCode)
                {
                    var results = resp.Content.ReadAsStringAsync().Result;
                    policyById = JsonConvert.DeserializeObject<PolicyVM>(results);
                    return View(policyById);
                }
                else if (resp.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    logger.Error($"Failed to retrieve policy details. Status code: {resp.StatusCode}");
                    return StatusCode((int)resp.StatusCode);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return View();
            }
        }
    }
}
