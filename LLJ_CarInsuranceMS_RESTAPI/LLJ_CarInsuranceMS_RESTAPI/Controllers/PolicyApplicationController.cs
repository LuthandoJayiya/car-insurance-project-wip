using LLJ_CarInsuranceMS_RESTAPI.AuthModels;
using LLJ_CarInsuranceMS_RESTAPI.Models;
using LLJ_CarInsuranceMS_RESTAPI.Repository;
using LLJ_CarInsuranceMS_RESTAPI.Services;
using LLJ_CarInsuranceMS_RESTAPI.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace LLJ_CarInsuranceMS_RESTAPI.Controllers
{
    /// <summary>
    /// Controller for managing policy applications.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyApplicationController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("PolicyApplicationController");
        private readonly LLJ_CarInsuranceMS_EFDBContext _context;
        private IdentityHelper _identityHelper;
        private UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationContext _authcontext;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Constructor for the PolicyApplicationController.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// /// <param name="authcontext">The authentication context.</param>
        /// /// <param name="userManager">The user manager.</param>
        /// /// <param name="roleManager">The Role manager.</param>
        public PolicyApplicationController(LLJ_CarInsuranceMS_EFDBContext context, UserManager<ApplicationUser> userManager,
            AuthenticationContext authcontext, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _authcontext = authcontext;
            _roleManager = roleManager;
            _identityHelper = new IdentityHelper(_userManager, _authcontext, _roleManager);
        }

        /// <summary>
        /// Retrieves all policy applications.
        /// </summary>
        /// <returns>A list of policy applications.</returns>
        // GET: api/PolicyApplications      
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PolicyApplication>>> GetAllPolicyApplications()
        {
            logger.Info("GetAllPolicyApplications - Retrieving all policy applications.");

            try
            {
                var polApp = await _context.PolicyApplications.ToListAsync();
                return Ok(polApp);
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while retrieving policy applications: {ex.Message}");
                return StatusCode(500);
            }
        }

        // GET: api/PolicyApplications/3
        /// <summary>
        /// Retrieves a specific policy application by its ID.
        /// </summary>
        /// <param name="id">The ID of the policy application to retrieve.</param>
        /// <returns>The policy application with the specified ID if found, otherwise returns NotFound.</returns>
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PolicyApplication>> GetPolicyApplication(int id)
        {
            logger.Info($"GetPolicyApplication - Retrieving a policy application with ID: {id}");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var polAppl = await _context.PolicyApplications.FindAsync(id);

                if (polAppl == null)
                {
                    logger.Warn($"GetPolicyApplication - policy application with ID {id} not found.");
                    return NotFound();
                }
                else
                {
                    logger.Info($"GetPolicyApplication - Policy Application with ID {id} retrieved successfully.");
                    return Ok(polAppl);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"GetPolicyApplication - An error occurred while retrieving policy application with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates the details of a specific policy application.
        /// </summary>
        /// <param name="id">The ID of the policy application.</param>
        /// <param name="polApp">The updated policy application details.</param>
        /// <returns>A response indicating the success or failure of the update operation.</returns>
        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPolicyApplication(int id, PolicyApplicationsVM polApp)
        {
            PolicyApplication updateThisApplication = new PolicyApplication()
            {
                ApplicationId = polApp.ApplicationId,
                CustomerId = polApp.CustomerId,
                PolicyId = polApp.PolicyId,
                ApplicationStatus = polApp.ApplicationStatus,
                RejectedReason = polApp.RejectedReason,
                ReviewedDate = polApp.ReviewedDate,
                ApprovedDate = polApp.ApprovedDate
                
            };
            if (id != updateThisApplication.ApplicationId)
            {
                return BadRequest();
            }

            _context.Entry(updateThisApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                logger.Info($"Updated appliaciton with ID: {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
                {
                    logger.Warn($"Application with ID {id} not found.");
                    return NotFound("Application was not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Message = "Application updated successfully" });
        }

        /// <summary>
        /// Creates a new Policy Application.
        /// </summary>
        /// <param name="polApp">The details of the Policy Application to be created.</param>
        /// <returns>Returns a newly created policy application along with a HTTP status code.</returns>
        // POST: api/PolicyApplications
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PolicyApplication>> PostPolicyApplication(PolicyApplicationsVM polApp)
        {

            logger.Info("CreatePolicyApplication - Creating a new policy application.");
            try
            {
                PolicyApplication newApplication = new PolicyApplication()
                {
                    CustomerId = polApp.CustomerId,
                    PolicyId = polApp.PolicyId,
                    SubmittedDate = polApp.SubmittedDate
                };

                _context.PolicyApplications.Add(newApplication);
                await _context.SaveChangesAsync();
                int currentId = newApplication.ApplicationId;

                logger.Info($"Created new policy application with ID: {currentId}");

                return CreatedAtAction("GetPolicyApplication", new { id = newApplication.ApplicationId }, newApplication);
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while creating a new Policy Application: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes an existing Policy Application.
        /// </summary>
        /// <param name="id">The ID of the Policy Application to be deleted.</param>
        /// <returns>Returns an HTTP status code indicating the result of the deletion operation.</returns>
        // DELETE: api/PolicyApplication/3
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<PolicyApplication>> DeletePolicyApplication(int id)
        {
            try
            {
                var appFound = await _context.PolicyApplications.FindAsync(id);
                if (appFound == null)
                {
                    return NotFound(new { Message = $"Policy Application with ID: {id} was not found" });
                }

                _context.PolicyApplications.Remove(appFound);
                await _context.SaveChangesAsync();

                logger.Info($"Deleted Policy Application with ID: {id}");

                return Ok(new { Message = "Policy Application deleted successfully" });
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while deleting Policy Application with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrives an application belonging to a certain customer
        /// </summary>
        /// <param name="customerId">The Customer ID of the Policy Application to look for.</param>
        /// <returns>Returns an HTTP status code indicating the result of the deletion operation.</returns>
        [HttpGet("Customer/{customerId}")]
        [Authorize]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerable<PolicyApplication>>> GetApplicationsByCustomerId(int customerId)
        {
            logger.Info($"Getting a list of applications for a certain Customer");
            var applications = await _context.PolicyApplications
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();

            if (applications == null)
            {
                logger.Warn($"Application for the Id: {customerId}, was not found");
                return NotFound();
            }

            return Ok(applications);
        }


        private bool ApplicationExists(int id)
        {
            return _context.PolicyApplications.Any(e => e.PolicyId == id);
        }
    }
}
