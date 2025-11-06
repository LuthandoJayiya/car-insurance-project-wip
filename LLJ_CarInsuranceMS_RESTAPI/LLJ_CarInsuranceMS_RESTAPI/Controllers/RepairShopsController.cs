using LLJ_CarInsuranceMS_RESTAPI.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LLJ_CarInsuranceMS_RESTAPI.Controllers
{
    /// <summary>
    /// API controller for managing repair shops.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RepairShopsController : ControllerBase
    {
        private readonly LLJ_CarInsuranceMS_EFDBContext _context;
        private static readonly ILog logger = LogManager.GetLogger("InsuranceAgentsController");

        /// <summary>
        /// Constructor for RepairShopsController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public RepairShopsController(LLJ_CarInsuranceMS_EFDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all repair shops.
        /// </summary>
        /// <returns>A list of repair shops.</returns>
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerable<RepairShop>>> GetRepairShops()
        {
            try
            {
                logger.Info("Retrieving all repair shops.");


                return await _context.RepairShops.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while retrieving all repair shops: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrieves a specific repair shop by ID.
        /// </summary>
        /// <param name="id">The ID of the repair shop.</param>
        /// <returns>The repair shop with the specified ID.</returns>
        [HttpGet("{id}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<RepairShop>> GetRepairShop(int id)
        {
            logger.Info($"Retrieving repair shop with ID: {id}");

            if (!ModelState.IsValid)
            {
                logger.Error("Invalid model state.");
                return BadRequest(ModelState);
            }

            var repairShop = await _context.RepairShops.FindAsync(id);

            if (repairShop == null)
            {
                logger.Warn($"Repair shop with ID {id} not found.");
                return NotFound();
            }
            else
            {
                return Ok(repairShop);
            }
        }

        /// <summary>
        /// Updates a repair shop with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the repair shop to update.</param>
        /// <param name="repairshop">The repair shop data to update.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("{id}")]
        [EnableCors("AllowOrigin")]
        [Authorize]
        public async Task<IActionResult> PutRepairShop(int id, RepairShop repairshop)
        {
            logger.Info($"Updating repair shop with ID: {id}");

            if (id != repairshop.ShopId)
            {
                logger.Warn("Invalid ID provided for repair shop update.");
                return BadRequest();
            }

            _context.Entry(repairshop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepairShopExists(id))
                {
                    logger.Warn($"Repair shop with ID {id} not found.");
                    return NotFound("Repair Shop was not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Message = "Repair Shop updated successfully" });
        }

        /// <summary>
        /// Creates a new repair shop.
        /// </summary>
        /// <param name="repairshop">The repair shop data to create.</param>
        /// <returns>An ActionResult containing the created repair shop.</returns>
        [HttpPost]
        [EnableCors("AllowOrigin")]
        [Authorize]
        public async Task<ActionResult<RepairShop>> PostRepairShop(RepairShop repairshop)
        {
            logger.Info("Creating a new repair shop");

            RepairShop newrepairShop = new RepairShop()
            {
                ShopName = repairshop.ShopName,
                Location = repairshop.Location,
                ContactInfo = repairshop.ContactInfo,
            };

            _context.RepairShops.Add(newrepairShop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRepairShop", new { id = newrepairShop.ShopId }, newrepairShop);
        }

        /// <summary>
        /// Deletes a repair shop with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the repair shop to delete.</param>
        /// <returns>An ActionResult indicating the result of the operation.</returns>
        [HttpDelete("{id}")]
        [EnableCors("AllowOrigin")]
        [Authorize]
        public async Task<ActionResult<RepairShop>> DeleteRepairShop(int id)
        {
            logger.Info($"Deleting repair shop with ID: {id}");
            try
            {
                var repairshop = await _context.RepairShops.FindAsync(id);
                if (repairshop == null)
                {
                    logger.Warn($"Repair shop with ID {id} not found.");
                    return NotFound(new { Message = "Repair Shop was not found" });
                }

                _context.RepairShops.Remove(repairshop);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Repair Shop deleted successfully" });
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while deleting Repair shop ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        private bool RepairShopExists(int id)
        {
            return _context.RepairShops.Any(e => e.ShopId == id);
        }
    }
}
