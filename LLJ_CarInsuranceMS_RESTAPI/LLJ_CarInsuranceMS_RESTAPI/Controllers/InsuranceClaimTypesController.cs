using LLJ_CarInsuranceMS_RESTAPI.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LLJ_CarInsuranceMS_RESTAPI.Controllers
{
    /// <summary>
    /// Controller for managing insurance claim types.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceClaimTypesController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("InsuranceAgentsController");
        private readonly LLJ_CarInsuranceMS_EFDBContext _context;

        /// <summary>
        /// Constructor for the InsuranceClaimTypesController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public InsuranceClaimTypesController(LLJ_CarInsuranceMS_EFDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all claim types.
        /// </summary>
        /// <returns>A list of insurance claim types.</returns>
        [HttpGet]
        [Route("InsuranceClaimTypes")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<InsuranceClaimType>>> GetInsuranceClaimTypes()
        {
            try
            {
                logger.Info("Retrieving list of insurance claim types.");

                return await _context.InsuranceClaimTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while retrieving insurance claim types: {ex.Message}");

                return StatusCode(500);
            }
        }



        /// <summary>
        /// Retrieves the details of a specific insurance claim type.
        /// </summary>
        /// <param name="id">The ID of the insurance claim type.</param>
        /// <returns>The details of the insurance claim type.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<InsuranceClaimType>> GetInsuranceClaimType(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.Warn("An error has occured while fetching data!");
                    return BadRequest(ModelState);
                }

                logger.Info($"Retrieving details of insurance claim type with ID: {id}");

                var claimType = await _context.InsuranceClaimTypes.FindAsync(id);

                if (claimType == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(claimType);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while retrieving details of insurance claim type with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates the details of a specific insurance claim type.
        /// </summary>
        /// <param name="id">The ID of the insurance claim type.</param>
        /// <param name="insuranceClaimType">The updated insurance claim type details.</param>
        /// <returns>A response indicating the success or failure of the update operation.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutInsuranceClaimType(int id, InsuranceClaimType insuranceClaimType)
        {
            try
            {
                if (id != insuranceClaimType.ClaimTypeId)
                {
                    return BadRequest();
                }

                _context.Entry(insuranceClaimType).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                logger.Info($"Updated details of insurance claim type with ID: {id}");

                return Ok(new { Message = "Insurance claim type updated successfully" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsuranceClaimTypeExists(id))
                {
                    return NotFound("Insurance claim type was not found");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while updating details of insurance claim type with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates a new insurance claim type.
        /// </summary>
        /// <param name="insuranceClaimType">The details of the insurance claim type to be created.</param>
        /// <returns>Returns a newly created insurance claim type along with a HTTP status code.</returns>
        // POST: api/InsuranceClaimType
        //[EnableCors]        // Default policy.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<InsuranceClaimType>> PostInsuranceClaimType(InsuranceClaimType insuranceClaimType)
        {
            try
            {
                InsuranceClaimType newInsuranceClaimType = new InsuranceClaimType()
                {
                    ClaimTypeName = insuranceClaimType.ClaimTypeName,
                    ClaimTypeDescription = insuranceClaimType.ClaimTypeDescription
                };

                _context.InsuranceClaimTypes.Add(newInsuranceClaimType);
                await _context.SaveChangesAsync();

                logger.Info($"Created a new insurance claim type with ID: {newInsuranceClaimType.ClaimTypeId}");

                return CreatedAtAction("GetInsuranceClaimType", new { id = newInsuranceClaimType.ClaimTypeId }, newInsuranceClaimType);
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while creating a new insurance claim type: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes an existing insurance claim type.
        /// </summary>
        /// <param name="id">The ID of the insurance claim type to be deleted.</param>
        /// <returns>Returns an HTTP status code indicating the result of the deletion operation.</returns>
        // DELETE: api/InsuranceClaimType/3
        //[EnableCors]        // Default policy.
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<InsuranceClaimType>> DeleteInsuranceClaimType(int id)
        {
            try
            {
                var insuranceClaimType = await _context.InsuranceClaimTypes.FindAsync(id);
                if (insuranceClaimType == null)
                {
                    return NotFound(new { Message = "Insurance claim type was not found" });
                }

                _context.InsuranceClaimTypes.Remove(insuranceClaimType);
                await _context.SaveChangesAsync();

                logger.Info($"Deleted insurance claim type with ID: {id}");

                return Ok(new { Message = "Insurance claim type deleted successfully" });
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while deleting insurance claim type with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }


        private bool InsuranceClaimTypeExists(int id)
        {
            return _context.InsuranceClaimTypes.Any(e => e.ClaimTypeId == id);
        }
    }
}
