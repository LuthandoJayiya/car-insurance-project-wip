using LLJ_CarInsuranceMS_ASPNET.Areas.Admin.Models;
using LLJ_CarInsuranceMS_ASPNET.Models;
using LLJ_CarInsuranceMS_ASPNET.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LLJ_CarInsuranceMS_ASPNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger("AdminController");

        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _httpClient;
        public AdminController(IOptions<ClientSettings> ctSettings, HttpClient injectedClient)
        {
            _clientSettings = ctSettings.Value;
            _httpClient = injectedClient;
        }
        public IActionResult Index()
        {
            logger.Info("Index");
            return View(); 
        }

        public async Task<IActionResult> UserProfile()
        {

            try
            {
                logger.Info("UserProfile");

                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrlProfile = baseUrl + "/api/UserProfile";
                string userHomePageUrl;

                UserProfileVM currentUserProfile = new UserProfileVM();

                string _userLoginToken = TempData["UserToken"]?.ToString();
                

                if (string.IsNullOrEmpty(_userLoginToken))
                {
                    logger.Warn("Token not provided or incorrect, redirecting to log in page");
                    TempData["UserHomePageUrl"] = "/Home/Index";
                    TempData.Keep();
                    return RedirectToAction("Login", "UserManager", new { area = "AuthServices" });
                }
                else
                {
                    TempData.Keep();
                    HttpClient _client = new HttpClient().AddBearerToken(_userLoginToken);

                    HttpResponseMessage resp = await _client.GetAsync(apiUrlProfile);
                    if (resp.IsSuccessStatusCode)
                    {
                        logger.Info("Getting User");
                        var result = resp.Content.ReadAsStringAsync().Result;
                        currentUserProfile = JsonConvert.DeserializeObject<UserProfileVM>(result);
                    }

                    logger.Info("Showing Current logged In User");
                    return View(currentUserProfile);
                }
            }
            catch (Exception e)
            {
                logger.Error($"An Error Occured while trying to retrieve User: {e.Message}");
                return View();
            }

        }

        public async Task<IActionResult> AllUsers()
        {
            try
            {
                logger.Info("AllUsers");
                List<UserProfileVM> allProfiles = new List<UserProfileVM>();
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrlProfile = baseUrl + "/api/UserProfile/AllUsers";
                string userHomePageUrl;

                string _userLoginToken = TempData["UserToken"]?.ToString();
                string? _userRole = TempData["UserRole"]?.ToString();

                if (string.IsNullOrEmpty(_userLoginToken))
                {
                    logger.Warn("Token not provided or incorrect, redirecting to log in page");
                    TempData["UserHomePageUrl"] = "/Home/Index";
                    TempData.Keep();
                    return RedirectToAction("Login", "UserManager", new { area = "AuthServices" });
                }
                else
                {
                    TempData.Keep();
                    HttpClient _client = new HttpClient().AddBearerToken(_userLoginToken);

                    HttpResponseMessage resp = await _client.GetAsync(apiUrlProfile);

                    if (resp.IsSuccessStatusCode)
                    {
                        logger.Info("Getting a List Of Users");
                        var result = resp.Content.ReadAsStringAsync().Result;
                        allProfiles = JsonConvert.DeserializeObject<List<UserProfileVM>>(result);
                    }
                    logger.Info("Showing List of Users");
                    return View(allProfiles);
                }
            }
            catch (Exception e)
            {
                logger.Error($"An Error Occured while trying to retrieve List of All Users: {e.Message}");
                return View();
            }

        }
    }
}
