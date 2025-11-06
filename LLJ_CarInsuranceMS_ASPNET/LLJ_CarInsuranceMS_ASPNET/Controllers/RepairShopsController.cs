using LLJ_CarInsuranceMS_ASPNET.Models;
using LLJ_CarInsuranceMS_ASPNET.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace LLJ_CarInsuranceMS_ASPNET.Controllers
{
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
                List<RepairShopVM> allReShops = new List<RepairShopVM>();
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrl = baseUrl + "/api/RepairShops";


                HttpResponseMessage resp = await _httpClient.GetAsync(apiUrl);

                if (resp.IsSuccessStatusCode)
                {
                    logger.Info("Retrieving List of Repair Shops");
                    var results = resp.Content.ReadAsStringAsync().Result;
                    allReShops = JsonConvert.DeserializeObject<List<RepairShopVM>>(results);
                }
                else
                {
                    logger.Warn("Failed to get list of Repair Shops");
                }

                logger.Info("Showing List of Repair Shops");
                return View(allReShops);
            }
            catch (Exception e)
            {
                logger.Error($"An Error Occured while trying to retrieve List of Repair Shops: {e.Message}");
                return View();
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            logger.Info("User has accessed the Details Page.");
            try
            {
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrl = baseUrl + $"/api/RepairShops/{id}";

                RepairShopVM shopById = new RepairShopVM();

                HttpResponseMessage resp = await _httpClient.GetAsync(apiUrl);
                if (resp.IsSuccessStatusCode)
                {
                    var results = resp.Content.ReadAsStringAsync().Result;
                    shopById = JsonConvert.DeserializeObject<RepairShopVM>(results);

                    logger.Info($"The Repair Shop with ID: {id} was found and is now being shown");
                    return View(shopById);
                }
                else if (resp.StatusCode == HttpStatusCode.NotFound)
                {
                    logger.Warn($"The Repair Shop with ID: {id} was not found");
                    return NotFound();
                }
                else
                {
                    logger.Error($"Failed to retrieve Repair Shop details. Status code: {resp.StatusCode}");
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
