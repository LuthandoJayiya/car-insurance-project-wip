using LLJ_CarInsuranceMS_RESTAPI.Models;
using LLJ_CarInsuranceMS_RESTAPI.Repository;
using LLJ_CarInsuranceMS_RESTAPI.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LLJ_CarInsuranceMS_RESTAPI.Controllers
{
    /// <summary>
    /// Controller for managing insurance claims.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceClaimsController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("InsuranceClaimsController");
        private readonly LLJ_CarInsuranceMS_EFDBContext _context;
        InsuranceClaimsRepo _repo;

        /// <summary>
        /// Constructor for the InsuranceClaimsController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public InsuranceClaimsController(LLJ_CarInsuranceMS_EFDBContext context)
        {
            _context = context;
            _repo = new InsuranceClaimsRepo(_context);
        }

        /// <summary>
        /// Retrieves all insurance claims.
        /// </summary>
        /// <returns>A list of insurance claims.</returns>
        // GET: api/InsuranceClaims       
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<InsuranceClaim>>> GetInsuranceClaims()
        {
            logger.Info("GetInsuranceClaims - Retrieving all insurance claims.");
            try
            {
                var claims = await _context.InsuranceClaims.ToListAsync();
                return Ok(claims);
            }
            catch (Exception ex)
            {
                logger.Error($"Error occurred while retrieving insurance claims: {ex.Message}");
                return StatusCode(500);
            }
        }


        // GET: api/InsuranceClaims/3
        /// <summary>
        /// Retrieves a specific insurance claim by its ID.
        /// </summary>
        /// <param name="id">The ID of the insurance claim to retrieve.</param>
        /// <returns>The insurance claim with the specified ID if found, otherwise returns NotFound.</returns>
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<InsuranceClaim>> GetInsuranceClaim(int id)
        {
            logger.Info($"GetInsuranceClaim - Retrieving insurance claim with ID: {id}");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var insuranceClaim = await _context.InsuranceClaims.FindAsync(id);

                if (insuranceClaim == null)
                {
                    logger.Warn($"GetInsuranceClaim - Insurance claim with ID {id} not found.");
                    return NotFound();
                }
                else
                {
                    logger.Info($"GetInsuranceClaim - Insurance claim with ID {id} retrieved successfully.");
                    return Ok(insuranceClaim);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"GetInsuranceClaim - An error occurred while retrieving insurance claim with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        // PUT: api/InsuranceClaim/3
        /// <summary>
        /// Updates an existing insurance claim with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the insurance claim to update.</param>
        /// <param name="insuranceClaim">The updated insurance claim data.</param>
        /// <returns>If the update is successful, returns Ok with a success message. If the insurance claim with the specified ID is not found, returns NotFound. If a concurrency exception occurs during the update, returns an error response.</returns>
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutInsuranceClaim(int id, InsuranceClaim insuranceClaim)
        {
            logger.Info($"PutInsuranceClaim - Updating insurance claim with ID: {id}");
            try
            {

                if (id != insuranceClaim.ClaimId)
                {
                    logger.Warn($"PutInsuranceClaim - ID in the URL ({id}) does not match the ID of the insurance claim object ({insuranceClaim.ClaimId}).");
                    return BadRequest();
                }

                _context.Entry(insuranceClaim).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    logger.Info($"PutInsuranceClaim - Insurance claim with ID {id} updated successfully.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceClaimExists(id))
                    {
                        logger.Warn($"PutInsuranceClaim - Insurance claim with ID {id} not found.");
                        return NotFound("Insurance Claim was not found");
                    }
                    else
                    {
                        logger.Error($"PutInsuranceClaim - Concurrency exception occurred while updating insurance claim with ID {id}.");
                        throw;
                    }
                }

                return Ok(new { Message = "Insurance Claim updated successfully" });
            }
            catch (Exception ex)
            {
                logger.Error($"PutInsuranceClaim - An error occurred while updating insurance claim with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        // POST: api/InsuranceClaim
        /// <summary>
        /// Creates a new insurance claim.
        /// </summary>
        /// <param name="insuranceClaim">The data for the new insurance claim.</param>
        /// <returns>If the creation is successful, returns CreatedAtAction with the newly created insurance claim. If an error occurs, returns an error response.</returns>
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<InsuranceClaim>> CreateInsuranceClaim(InsuranceClaimVM insuranceClaim)
        {
            logger.Info("CreateInsuranceClaim - Creating a new insurance claim.");
            try
            {
                InsuranceClaim newInsuranceClaim = new InsuranceClaim()
                {
                    ClaimName = insuranceClaim.ClaimName,
                    AccidentDate = insuranceClaim.AccidentDate,
                    ClaimStatus = insuranceClaim.ClaimStatus
                };

                _context.InsuranceClaims.Add(newInsuranceClaim);
                await _context.SaveChangesAsync();
                int currentClaimId = newInsuranceClaim.ClaimId;

                logger.Info($"CreateInsuranceClaim - Insurance claim created successfully with ID: {currentClaimId}");
                return CreatedAtAction(nameof(GetInsuranceClaim), new { id = newInsuranceClaim.ClaimId }, newInsuranceClaim);
            }
            catch (Exception ex)
            {
                logger.Error($"CreateInsuranceClaim - An error occurred while creating a new insurance claim: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes a specific insurance claim.
        /// </summary>
        /// <param name="id">The ID of the insurance claim, To delete.</param>
        /// <returns>Ok message if the Insurance claim was deleted successfully, otherwise BadRequest.</returns>
        // DELETE: api/DeleteInsuranceClaim/3
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<InsuranceClaim>> DeleteInsuranceClaim(int id)
        {
            logger.Info($"DeleteInsuranceClaim - Deleting insurance claim with ID: {id}");
            try
            {
                var insuranceClaim = await _context.InsuranceClaims.FindAsync(id);
                if (insuranceClaim == null)
                {
                    logger.Warn($"DeleteInsuranceClaim - Insurance claim with ID {id} not found.");
                    return NotFound(new { Message = "Insurance Claim was not found" });
                }

                _context.InsuranceClaims.Remove(insuranceClaim);
                await _context.SaveChangesAsync();

                logger.Info($"DeleteInsuranceClaim - Insurance claim with ID {id} deleted successfully.");
                return Ok(new { Message = "Insurance Claim deleted successfully" });
            }
            catch (Exception ex)
            {
                logger.Error($"DeleteInsuranceClaim - An error occurred while deleting insurance claim with ID {id}: {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrieves all insurance claims along with their associated drivers.
        /// </summary>
        /// <returns>A list of insurance claims with drivers.</returns>
        [Route("InsuranceClaimsDrivers")]
        //GET:  api/InsuranceClaims/InsuranceClaimsDrivers
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<InsuranceClaimsDriverVM>>> GetAllInsuranceClaimsWithDrivers()
        {
            logger.Info("GetAllInsuranceClaimsWithDrivers - Retrieving all insurance claims with drivers.");

            try
            {
                List<InsuranceClaimsDriverVM> allIC = _repo.GetAllInsuranceClaimsDrivers();
                return allIC;
            }
            catch (Exception ex)
            {
                logger.Error($"GetAllInsuranceClaimsWithDrivers - An error occurred while retrieving insurance claims with drivers: {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrieves a specific insurance claim along with its associated driver.
        /// </summary>
        /// <param name="id">The ID of the insurance claim.</param>
        /// <returns>The insurance claim with the specified ID along with its associated driver if found, otherwise NotFound.</returns>
        //GET:  api/InsuranceClaims/InsuranceClaimDriver/1
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpGet("InsuranceClaimDriver/{id}")]
        [Authorize]
        public async Task<ActionResult<InsuranceClaimsDriverVM>> GetOneInsuranceClaimWithDriver(int id)
        {
            logger.Info($"GetOneInsuranceClaimWithDriver - Retrieving insurance claim with driver for ID: {id}");

            try
            {
                InsuranceClaimsDriverVM theClaimDriver = new InsuranceClaimsDriverVM();
                List<InsuranceClaimsDriverVM> allCD = _repo.GetAllInsuranceClaimsDrivers();
                var claimDriverQuery = allCD.Where(x => x.ClaimId == id);
                theClaimDriver = claimDriverQuery.FirstOrDefault();

                if (theClaimDriver == null)
                {
                    logger.Warn($"GetOneInsuranceClaimWithDriver - No insurance claim with driver found for ID: {id}");
                    return NotFound();
                }

                return theClaimDriver;
            }
            catch (Exception ex)
            {
                logger.Error($"GetOneInsuranceClaimWithDriver - An error occurred while retrieving insurance claim with driver for ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrieves all insurance claims along with their associated surveyors.
        /// </summary>
        /// <returns>A list of insurance claims with surveyors.</returns>
        [Route("InsuranceClaimsSurveyors")]
        //GET:  api/InsuranceClaims/InsuranceClaimsSurveyors
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<InsuranceClaimsSurveyorVM>>> GetAllInsuranceClaimsWithSurveyors()
        {
            try
            {
                logger.Info("GetAllInsuranceClaimsWithSurveyors - Retrieving all insurance claims with surveyors.");
                List<InsuranceClaimsSurveyorVM> allIS = _repo.GetAllInsuranceClaimsSurveyors();
                return allIS;
            }
            catch (Exception ex)
            {
                logger.Error($"GetAllInsuranceClaimsWithSurveyors - An error occurred while retrieving insurance claims with surveyors: {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrieves a specific insurance claim along with its associated surveyor.
        /// </summary>
        /// <param name="id">The ID of the insurance claim.</param>
        /// <returns>The insurance claim with the specified ID along with its associated surveyor if found, otherwise NotFound.</returns>
        //GET:  api/InsuranceClaims/InsuranceClaimSurveyor/1
        [EnableCors("AllowOrigin")]
        [HttpGet("InsuranceClaimSurveyor/{id}")]
        [Authorize]
        public async Task<ActionResult<InsuranceClaimsSurveyorVM>> GetOneInsuranceClaimWithSurveyor(int id)
        {
            try
            {
                logger.Info($"GetOneInsuranceClaimWithSurveyor - Retrieving insurance claim with surveyor for ID: {id}");
                InsuranceClaimsSurveyorVM theClaimSurveyor = new InsuranceClaimsSurveyorVM();
                List<InsuranceClaimsSurveyorVM> allCS = _repo.GetAllInsuranceClaimsSurveyors();
                var claimSurveyorQuery = allCS.Where(x => x.ClaimId == id);
                theClaimSurveyor = claimSurveyorQuery.FirstOrDefault();

                if (theClaimSurveyor == null)
                {
                    logger.Warn($"GetOneInsuranceClaimWithSurveyor - No insurance claim with surveyor found for ID: {id}");
                    return NotFound();
                }

                return theClaimSurveyor;
            }
            catch (Exception ex)
            {
                logger.Error($"GetOneInsuranceClaimWithSurveyor - An error occurred while retrieving insurance claim with surveyor for ID {id}: {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrieves all insurance claims along with their associated repair shops.
        /// </summary>
        /// <returns>A list of insurance claims with repair shops.</returns>
        [Route("InsuranceClaimsRepairshops")]
        //GET:  api/InsuranceClaims/InsuranceClaimsRepairshops
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<InsuranceClaimRepairShopVM>>> GetAllInsuranceClaimsWithRepairshops()
        {
            try
            {
                logger.Info("GetAllInsuranceClaimsWithRepairshops - Retrieving all insurance claims with repair shops.");
                List<InsuranceClaimRepairShopVM> allIR = new List<InsuranceClaimRepairShopVM>();
                allIR = _repo.GetAllInsuranceClaimsRepairshops();
                return allIR;
            }
            catch (Exception ex)
            {
                logger.Error($"GetAllInsuranceClaimsWithRepairshops - An error occurred while retrieving insurance claims with repair shops: {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrieves a specific insurance claim along with its associated repair shop.
        /// </summary>
        /// <param name="id">The ID of the insurance claim.</param>
        /// <returns>The insurance claim with the specified ID along with its associated repair shop if found, otherwise NotFound.</returns>
        //GET:  api/InsuranceClaims/InsuranceClaimRepairshop/1
        [EnableCors("AllowOrigin")]
        [HttpGet("InsuranceClaimRepairshop/{id}")]
        [Authorize]
        public async Task<ActionResult<InsuranceClaimRepairShopVM>> GetOneInsuranceClaimWithRepairshop(int id)
        {
            try
            {
                logger.Info($"GetOneInsuranceClaimWithRepairshop - Retrieving insurance claim with repair shop for ID: {id}");
                InsuranceClaimRepairShopVM theClaimRS = new InsuranceClaimRepairShopVM();
                List<InsuranceClaimRepairShopVM> allCR = new List<InsuranceClaimRepairShopVM>();
                allCR =  _repo.GetAllInsuranceClaimsRepairshops();

                var claimRSQuery = allCR.Where(x => x.ClaimId == id);
                theClaimRS = claimRSQuery.FirstOrDefault();

                if (theClaimRS == null)
                {
                    logger.Warn($"GetOneInsuranceClaimWithRepairshop - No insurance claim with repair shop found for ID: {id}");
                    return NotFound();
                }

                return theClaimRS;
            }
            catch (Exception ex)
            {
                logger.Error($"GetOneInsuranceClaimWithRepairshop - An error occurred while retrieving insurance claim with repair shop for ID {id}: {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Checks if an insurance claim with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the insurance claim to check.</param>
        /// <returns>True if the insurance claim exists, otherwise false.</returns>
        private bool InsuranceClaimExists(int id)
        {
            return _context.InsuranceClaims.Any(e => e.ClaimId == id);
        }
    }
}
