using LLJ_CarInsuranceMS_RESTAPI.AuthModels;
using LLJ_CarInsuranceMS_RESTAPI.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LLJ_CarInsuranceMS_RESTAPI.Controllers
{
    /// <summary>
    /// Controller for managing user profiles.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class UserProfileController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("CustomersController");
        private IdentityHelper _identityHelper;
        private UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Constructor for UserProfileController.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="context">The authentication context.</param>
        /// <param name="roleManager">The role manager.</param>
        public UserProfileController(UserManager<ApplicationUser> userManager,
            AuthenticationContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _identityHelper = new IdentityHelper(_userManager, _context, _roleManager);
        }

        /// <summary>
        /// Gets the profile information of the authenticated user.
        /// </summary>
        /// <returns>The profile information.</returns>
        [EnableCors("AllowOrigin")]
        [HttpGet]
        // Get : /api/UserProfile
        public async Task<Object> Get()
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                List<string> userRoles = new List<string>(await _userManager.GetRolesAsync(user));

                return new
                {
                    user.FullName,
                    user.PhoneNumber,
                    user.LicenseNumber,
                    user.Email,
                    user.UserName,
                    userRoles
                };
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while retrieving user profile: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Gets all users' information if the current user is authorized to do so.
        /// </summary>
        /// <returns>The list of users' information.</returns>
        [EnableCors("AllowOrigin")]
        [Authorize]
        [HttpGet("AllUsers")]
        // Get : /api/AllUsers
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                bool rightRole = await _identityHelper.IsSuperUserRole(userId);

                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(new { message = "You are not authorized to view this information" });
                }
                else
                {
                    if (rightRole)
                    {
                        var users = await _userManager.Users.ToListAsync();

                        var AllUsers = users.Select(async u => new
                        {
                            FullName = u.FullName,
                            Email = u.Email,
                            UserName = u.UserName,
                            Roles = await _userManager.GetRolesAsync(u)
                        });

                        return Ok(AllUsers);
                    }
                    else { return Forbid(); }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while retrieving all users: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while processing your request" });
            }
        }
    }
}

