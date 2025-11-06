using LLJ_CarInsuranceMS_ASPNET.Areas.AuthServices.Models;
using LLJ_CarInsuranceMS_ASPNET.Services;
using log4net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace LLJ_CarInsuranceMS_ASPNET.Areas.AuthServices.Controllers
{
    [Area("AuthServices")]
    public class UserManagerController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger("UserManagerController");


        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _httpClient;

        public UserManagerController(IOptions<ClientSettings> ctSettings, HttpClient injectedClient)
        {
            _clientSettings = ctSettings.Value;
            _httpClient = injectedClient;
        }

        public IActionResult Index()
        {
            logger.Info("User has accessed the Index page.");
            return View();
        }

        public IActionResult Register()
        {
            logger.Info("User has accessed the Register page.");
            return View();
        }

        // POST: Register page
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationVM usrRegData)
        {
            logger.Info("User submitted the Register form.");
            try
            {
                UserRegisteredVM newlyRegUser = new UserRegisteredVM();
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrlRegister = baseUrl + "/api/ApplicationUser/Register";

                logger.Debug($"Sending POST request to {apiUrlRegister} with user registration data for user {newlyRegUser.UserName}");

                var postResponse = await _httpClient.PostAsJsonAsync(apiUrlRegister, usrRegData);
                logger.Info($"Received response with status code {postResponse.StatusCode} from {apiUrlRegister} for user {newlyRegUser.UserName}");

                ViewBag.POSTresultHeader = postResponse.Headers;
                ViewBag.POSTresultStatusCode = postResponse.StatusCode;
                ViewBag.POSTresultRequestMessage = postResponse.RequestMessage;
                ViewBag.POSTresultIsSuccessStatusCode = postResponse.IsSuccessStatusCode;

                if (postResponse.IsSuccessStatusCode)
                {
                    var results = await postResponse.Content.ReadAsStringAsync();
                    newlyRegUser = JsonConvert.DeserializeObject<UserRegisteredVM>(results);
                    logger.Info($"User registration successful for user {newlyRegUser.UserName}.");
                }
                else
                {
                    logger.Warn($"User registration failed with status code {postResponse.StatusCode}.");
                }

                TempData["POSTRegUserUserName"] = newlyRegUser.UserName;
                TempData.Keep();

                logger.Info("Redirecting to the Index page.");
                return RedirectToAction("Index", "Home", new { area = "default" });
            }
            catch (Exception e)
            {
                logger.Error($"An error occurred while registering the user: {e.Message}");
                return View();
            }
        }

        public IActionResult Login()
        {
            logger.Info("User has accessed the Login page.");
            return View();
        }

        // POST: Login page
        [HttpPost]
        public IActionResult Login(LoginVM usrLogin)
        {
            logger.Info("User submitted the Login form.");
            try
            {
                CurrentUserVM currentLoggedInUser = new CurrentUserVM();
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrlLogin = baseUrl + "/api/ApplicationUser/Login";

                logger.Debug($"Sending POST request to {apiUrlLogin} with user registration data for user {currentLoggedInUser.UserName}");

                //Set home url for user after successful login
                string userHomePageUrl = baseUrl + "/Home/Index";

                var postResponse = _httpClient.PostAsJsonAsync<LoginVM>(apiUrlLogin, usrLogin);
                postResponse.Wait();
                var result = postResponse.Result;
                logger.Info($"Received response with status code {postResponse.Result.StatusCode} from {apiUrlLogin} for user {currentLoggedInUser.UserName}");


                ViewBag.POSTresultHeader = result.Headers;
                ViewBag.POSTresultStatusCode = result.StatusCode;
                ViewBag.POSTresultRequestMessage = result.RequestMessage;
                ViewBag.POSTresultIsSuccessStatusCode = result.IsSuccessStatusCode;


                if (result.IsSuccessStatusCode)
                {
                    var results = result.Content.ReadAsStringAsync().Result;
                    currentLoggedInUser = JsonConvert.DeserializeObject<CurrentUserVM>(results);
                    logger.Info($"User Login successful for user {currentLoggedInUser.UserName}.");
                }
                else
                {
                    logger.Warn($"User Login failed with status code {postResponse.Result.StatusCode}.");
                }

                //storing data into tempdata  
                TempData["UserHomePageUrl"] = userHomePageUrl;
                TempData["UserToken"] = currentLoggedInUser.Token;
                TempData["UserTokenValidTo"] = currentLoggedInUser.Expiration;
                TempData["FullName"] = currentLoggedInUser.FullName;
                TempData["UserName"] = currentLoggedInUser.UserName;
                TempData["UserRole"] = currentLoggedInUser.Roles[0];
                TempData.Keep();

                if (result.IsSuccessStatusCode)
                {
                    if (currentLoggedInUser != null && currentLoggedInUser.Roles[0] == "Administrator")
                    {
                        logger.Info("Redirecting to the Admin page.");
                        TempData["UserHomePageUrl"] = "/Admin/Home/Index";
                        TempData.Keep();
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }
                    else if (currentLoggedInUser != null && currentLoggedInUser.Roles[0] == "Driver")
                    {
                        logger.Info("Redirecting to the Driver Home Page.");
                        TempData["UserHomePageUrl"] = "/Drivers/Drivers/Index";
                        string? homepageUrl = TempData["UserHomePageUrl"]?.ToString();
                        TempData.Keep();
                        return RedirectToAction("Index", "Drivers", new { area = "Drivers" });
                    }
                    else if (currentLoggedInUser != null && currentLoggedInUser.Roles[0] == "ClaimSurveyor")
                    {
                        logger.Info("Redirecting to the Surveyor Home Page.");
                        TempData["UserHomePageUrl"] = "/Customer/Home/Index";
                        string? homepageUrl = TempData["UserHomePageUrl"]?.ToString();
                        TempData.Keep();
                        return RedirectToAction("Index", "Surveyors", new { area = "ClaimSurveyor" });
                    }
                    else if (currentLoggedInUser != null && currentLoggedInUser.Roles[0] == "RepairShop")
                    {
                        logger.Info("Redirecting to the Repair Shop Page.");
                        TempData["UserHomePageUrl"] = "/Customer/Home/Index";
                        string? homepageUrl = TempData["UserHomePageUrl"]?.ToString();
                        TempData.Keep();
                        return RedirectToAction("Index", "RepairShop", new { area = "RepairShop" });
                    }
                    else if (currentLoggedInUser != null && currentLoggedInUser.Roles[0] == "PotentialCustomer")
                    {
                        logger.Info("Redirecting to the Customer page.");
                        TempData["UserHomePageUrl"] = "/Customer/Home/Index";
                        string? homepageUrl = TempData["UserHomePageUrl"]?.ToString();
                        TempData.Keep();
                        return RedirectToAction("Index", "Customers", new { area = "Customers" });
                    }
                    else
                    {
                        TempData["UserHomePageUrl"] = "/Home/Index";
                        return RedirectToAction("Index", "Home", new { area = "default" });
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                return View();
            }

        }

        public IActionResult Logout()
        {
            logger.Info("Logout");
            // Clear TempData
            TempData.Clear();

            // Redirect to the landing page
            return RedirectToAction("Index", "Home", new {area = "default"});
        }
    }
}
