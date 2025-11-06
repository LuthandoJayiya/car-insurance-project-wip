using LLJ_CarInsuranceMS_ASPNET.Models;
using LLJ_CarInsuranceMS_ASPNET.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LLJ_CarInsuranceMS_ASPNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CrudPoliciesController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger("CrudPoliciesController");

        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _httpClient;

        public CrudPoliciesController(IOptions<ClientSettings> ctSettings, HttpClient injectedClient)
        {
            _clientSettings = ctSettings.Value;
            _httpClient = injectedClient;
        }
        public async Task<IActionResult> Index()
        {

            logger.Info("User has accessed the Index page.");
            try
            {
                List<Policy> allPolicies = new List<Policy>();
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrl = baseUrl + "/api/Policies";

                string? _userLoginToken = TempData["UserToken"]?.ToString();
                string? _tokenValidation = TempData["UserTokenValidTo"]?.ToString();
                TempData.Keep();
                _httpClient.AddBearerToken(_userLoginToken);

                HttpResponseMessage resp = await _httpClient.GetAsync(apiUrl);

                if (resp.IsSuccessStatusCode)
                {
                    logger.Info("Retrieving List of Policies");
                    var results = resp.Content.ReadAsStringAsync().Result;
                    allPolicies = JsonConvert.DeserializeObject<List<Policy>>(results);
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Policy policy)
        {
            try
            {
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrl = baseUrl + "/api/Policies";

                string? _userLoginToken = TempData["UserToken"]?.ToString();
                string? _tokenValidation = TempData["UserTokenValidTo"]?.ToString();
                TempData.Keep();
                _httpClient.AddBearerToken(_userLoginToken);

                var postResponse = _httpClient.PostAsJsonAsync<Policy>(apiUrl, policy);
                postResponse.Wait();
                var result = postResponse.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Policy successfully created.";
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch { return View(); }
        }

        public async Task<IActionResult> Update(int id)
        {
            Policy policy = await GetPolicyById(id);

            return View(policy);
        }

        // POST: CrudPolicy/Update/3
        [HttpPost]
        public ActionResult Update(Policy policy)
        {
            try
            {
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrl = baseUrl + $"/api/Policies/{policy.PolicyId}";

                string? _userLoginToken = TempData["UserToken"]?.ToString();
                string? _tokenValidation = TempData["UserTokenValidTo"]?.ToString();
                TempData.Keep();
                _httpClient.AddBearerToken(_userLoginToken);

                var putResponse = _httpClient.PutAsJsonAsync(apiUrl, policy);
                putResponse.Wait();
                var result = putResponse.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Policy policy = await GetPolicyById(id);

            return View(policy);
        }

        // POST: CrudPolicies/Delete/5
        [HttpPost]
        public ActionResult Delete(Policy policy)
        {
            try
            {
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrl = baseUrl + $"/api/Policies/{policy.PolicyId}";

                string? _userLoginToken = TempData["UserToken"]?.ToString();
                string? _tokenValidation = TempData["UserTokenValidTo"]?.ToString();
                TempData.Keep();
                _httpClient.AddBearerToken(_userLoginToken);

                var delResponse = _httpClient.DeleteAsync(apiUrl);
                delResponse.Wait();
                var result = delResponse.Result;


                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }


        private async Task<Policy> GetPolicyById(int id)
        {
            string baseUrl = _clientSettings.ClientBaseUrl;
            string apiUrl = baseUrl + $"/api/Policies/{id}";

            string? _userLoginToken = TempData["UserToken"]?.ToString();
            string? _tokenValidation = TempData["UserTokenValidTo"]?.ToString();
            TempData.Keep();
            _httpClient.AddBearerToken(_userLoginToken);

            Policy policy = new Policy();

            HttpResponseMessage resp = await _httpClient.GetAsync(apiUrl);
            if (resp.IsSuccessStatusCode)
            {
                var results = resp.Content.ReadAsStringAsync().Result;
                policy = JsonConvert.DeserializeObject<Policy>(results);
            }
            return policy;
        }
    }
}
