using LLJ_CarInsuranceMS_RESTAPI.AuthModels;
using LLJ_CarInsuranceMS_RESTAPI.Models;
using LLJ_CarInsuranceMS_RESTAPI.ViewModel;
using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LLJ_CarInsuranceMS_RESTAPI.Controllers
{
    /// <summary>
    /// This controller contain the end points for user registration and login
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("ApplicationUserController");

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private readonly LLJ_CarInsuranceMS_EFDBContext _context;

        /// <summary>
        /// Constructor for the ApplicationUserController.
        /// </summary>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="appSettings">The Application settings.</param>
        /// <param name="context">The Application context.</param>
        public ApplicationUserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings, LLJ_CarInsuranceMS_EFDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _context = context;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// /// <param name="model">The application user model.</param>
        /// <returns>If the creation is successful, returns ok with the newly created user's username. If an error occurs, returns an error response.</returns>

        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Route("Register")]
        // Post : /api/ApplicationUser/Register
        public async Task<Object> PostApplicationUser(ApplicationUserModel model)
        {
            logger.Info("ApplicationUserController - Post : /api/ApplicationUser/Register");
            logger.Info("ApplicationUserController - Post : /api/ApplicationUser/Register model.UserName:" + model.UserName);
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                LicenseNumber = model.LicenseNumber
            };

            if (model.Role == null || model.Role == "")
            {  //Set default Role
                model.Role = "PotentialCustomer";
            }

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);

                if (result.Succeeded)
                {
                    var userResult = await _userManager.AddToRoleAsync(applicationUser, model.Role);
                    if (model.Role == "PotentialCustomer")
                    {
                        var potentialCustomer = new PotentialCustomerVM
                        {
                            CustomerName = applicationUser.FullName,
                            CustomerPhone = applicationUser.PhoneNumber,
                            CustomerEmail = applicationUser.Email,
                            IdentintyUsername = applicationUser.UserName,
                        };

                        PotentialCustomer newCustomer = new PotentialCustomer
                        {
                            CustomerName = potentialCustomer.CustomerName,
                            CustomerPhone = potentialCustomer.CustomerPhone,
                            CustomerEmail = potentialCustomer.CustomerEmail,
                            IdentintyUsername = potentialCustomer.IdentintyUsername
                        };

                        _context.PotentialCustomers.Add(newCustomer);
                        await _context.SaveChangesAsync();
                    }
                    return Ok(new { Username = applicationUser.UserName });
                }
                return BadRequest(new { message = "ERROR Creating user: Username or password not VALID." });
            }
            catch (Exception e)
            {

                return BadRequest(new { message = "ERROR Creating user: Username or password not VALID." + e });
            }
        }

        /// <summary>
        /// Logs in a user and generates a JWT token for authentication.
        /// </summary>
        /// <param name="model">The login model containing username and password.</param>
        /// <returns>Action result with JWT token and user information.</returns>
        [HttpPost]
        [Route("Login")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            logger.Info("Post : /api/ApplicationUser/Login");
            logger.Info("Post : /api/ApplicationUser/Login model.UserName:" + model.UserName);

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserID", user.Id.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.SigningKey));
                int expiryInMinutes = Convert.ToInt32(_appSettings.ExpiryInMinutes);

                var token = new JwtSecurityToken(
                    issuer: _appSettings.JWT_Site_URL,
                    audience: _appSettings.JWT_Site_URL,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    roles = await _userManager.GetRolesAsync(user)
                });
            }
            else
            {
                return BadRequest(new { message = "Username or password not found." });
            }
        }

    }
}
