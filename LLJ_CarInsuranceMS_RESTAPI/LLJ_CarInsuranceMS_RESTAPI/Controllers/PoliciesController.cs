using LLJ_CarInsuranceMS_RESTAPI.AuthModels;
using LLJ_CarInsuranceMS_RESTAPI.Models;
using LLJ_CarInsuranceMS_RESTAPI.Repository;
using LLJ_CarInsuranceMS_RESTAPI.Services;
using LLJ_CarInsuranceMS_RESTAPI.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LLJ_CarInsuranceMS_RESTAPI.Controllers
{
    /// <summary>
    /// Controller for managing policies.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("InsuranceClaimsController");
        private readonly LLJ_CarInsuranceMS_EFDBContext _context;
        InsuranceClaimsRepo _repo;
        private IdentityHelper _identityHelper;
        private UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationContext _authcontext;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Constructor for the PolicyController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PoliciesController(LLJ_CarInsuranceMS_EFDBContext context, UserManager<ApplicationUser> userManager,
            AuthenticationContext authcontext, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _authcontext = authcontext;
            _roleManager = roleManager;
            _identityHelper = new IdentityHelper(_userManager, _authcontext, _roleManager);
            _repo = new InsuranceClaimsRepo(_context);
        }

        /// <summary>
        /// Retrieves all policies.
        /// </summary>
        /// <returns>A list of policies.</returns>
        // GET: api/Policies       
        [EnableCors("AllowOrigin")]
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Policy>>> GetAllPolicies()
        {
            logger.Info("GetAllPolicies - Retrieving all policies.");

            try
            {
                var policies = await _context.Policies.ToListAsync();
                return Ok(policies);
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while retrieving policies: {ex.Message}");
                return StatusCode(500);
            }
        }


        // GET: api/Policy/3
        /// <summary>
        /// Retrieves a specific policy by its ID.
        /// </summary>
        /// <param name="id">The ID of the policy to retrieve.</param>
        /// <returns>The policy with the specified ID if found, otherwise returns NotFound.</returns>
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<Policy>> GetPolicy(int id)
        {
            logger.Info($"GetPolicy - Retrieving a policy with ID: {id}");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var policy = await _context.Policies.FindAsync(id);

                if (policy == null)
                {
                    logger.Warn($"GetPolicy - policy with ID {id} not found.");
                    return NotFound();
                }
                else
                {
                    logger.Info($"GetPolicy - Policy with ID {id} retrieved successfully.");
                    return Ok(policy);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"GetPolicy - An error occurred while retrieving policy with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Updates the details of a specific policy.
        /// </summary>
        /// <param name="id">The ID of the policy.</param>
        /// <param name="policy">The updated policy details.</param>
        /// <returns>A response indicating the success or failure of the update operation.</returns>
        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPolicy(int id, PolicyVM policy)
        {
            Policy newPolicy = new Policy()
            {
                PolicyId = policy.PolicyId,
                PolicyType = policy.PolicyType,
                PolicyName = policy.PolicyName,
                Coverage = policy.Coverage,
                Premium = policy.Premium,
                ExpirationDate = policy.ExpirationDate,
                CreationDate = policy.CreationDate

            };

            if (id != newPolicy.PolicyId)
            {
                return BadRequest();
            }

            _context.Entry(newPolicy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                logger.Info($"Updated Policy with ID: {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(id))
                {
                    logger.Warn($"Policy with ID {id} not found.");
                    return NotFound("Policy was not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Message = "Policy updated successfully" });
        }

        /// <summary>
        /// Creates a new Policy.
        /// </summary>
        /// <param name="policy">The details of the Policy to be created.</param>
        /// <returns>Returns a newly created insurance agent along with a HTTP status code.</returns>
        // POST: api/Policy
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Policy>> PostPolicy(PolicyVM policy)
        {

            logger.Info("CreatePolicy - Creating a new policy.");
            try
            {
                Policy newPolicy = new Policy()
                {
                    PolicyType = policy.PolicyType,
                    PolicyName = policy.PolicyName,
                    Coverage = policy.Coverage,
                    Premium = policy.Premium,
                    ExpirationDate = policy.ExpirationDate,
                    CreationDate = policy.CreationDate
                };

                _context.Policies.Add(newPolicy);
                await _context.SaveChangesAsync();
                int currentId = newPolicy.PolicyId;

                logger.Info($"Created new policy with ID: {currentId}");

                return CreatedAtAction("GetPolicy", new { id = newPolicy.PolicyId }, newPolicy);
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while creating a new Policy: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes an existing Policy.
        /// </summary>
        /// <param name="id">The ID of the Policy to be deleted.</param>
        /// <returns>Returns an HTTP status code indicating the result of the deletion operation.</returns>
        // DELETE: api/Policy/3
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Policy>> DeletePolicy(int id)
        {
            try
            {
                var policyFound = await _context.Policies.FindAsync(id);
                if (policyFound == null)
                {
                    return NotFound(new { Message = "Policy was not found" });
                }

                _context.Policies.Remove(policyFound);
                await _context.SaveChangesAsync();

                logger.Info($"Deleted Policy with ID: {id}");

                return Ok(new { Message = "Policy deleted successfully" });
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while deleting Policy with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }


        private bool PolicyExists(int id)
        {
            return _context.Policies.Any(e => e.PolicyId == id);
        }
    }
}
