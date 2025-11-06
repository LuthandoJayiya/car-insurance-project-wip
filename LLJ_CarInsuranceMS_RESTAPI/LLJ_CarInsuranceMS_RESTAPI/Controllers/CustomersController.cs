using LLJ_CarInsuranceMS_RESTAPI.Models;
using LLJ_CarInsuranceMS_RESTAPI.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LLJ_CarInsuranceMS_RESTAPI.Controllers
{
    /// <summary>
    /// Controller for managing potential customers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private static readonly ILog logger = LogManager.GetLogger("CustomersController");
        private readonly LLJ_CarInsuranceMS_EFDBContext _context;

         /// <summary>
        /// Constructor for the CustomersController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CustomersController(LLJ_CarInsuranceMS_EFDBContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>Returns a list of all customers.</returns>
        // GET: api/Customers        
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PotentialCustomer>>> GetCustomers()
        {
            try
            {
                logger.Info("Retrieving all customers.");

                return await _context.PotentialCustomers.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while retrieving all customers: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrieves a specific customer by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>Returns the customer with the specified ID.</returns>
        // GET: api/Customers/5
        [EnableCors]        // Default policy.
        [HttpGet("{id}")]
        public async Task<ActionResult<PotentialCustomer>> GetCustomer(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var customer = await _context.PotentialCustomers.FindAsync(id);

                if (customer == null)
                {
                    logger.Warn($"Customer with ID {id} not found.");
                    return NotFound();
                }
                else
                {
                    logger.Info($"Retrieved customer with ID: {id}");
                    return Ok(customer);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while retrieving customer with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="customerVM">The updated customer information.</param>
        /// <returns>Returns a HTTP status code indicating the result of the update operation.</returns>
        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [EnableCors("AllowOrigin")]        // Default policy.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, PotentialCustomerVM customerVM)
        {
            PotentialCustomer customer = new PotentialCustomer()
            {
                CustomerId = customerVM.CustomerId,
                CustomerName = customerVM.CustomerName,
                CustomerEmail = customerVM.CustomerEmail,
                CustomerPhone = customerVM.CustomerPhone,
                CustomerCity = customerVM.CustomerCity,
                CustomerCountry = customerVM.CustomerCountry
            };

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                logger.Info($"Updated customer with ID: {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    logger.Warn($"Customer with ID {id} not found.");
                    return NotFound("Customer was not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Message = "Customer updated successfully" });
        }

        /// <summary>
        /// Creates a new customer in the database.
        /// </summary>
        /// <param name="customer">The customer object to create.</param>
        /// <returns>An ActionResult of PotentialCustomer.</returns>
        [HttpPost]
        [EnableCors("AllowOrigin")]
        [Authorize]
        public async Task<ActionResult<PotentialCustomer>> PostCustomers(PotentialCustomerVM customer)
        {
            logger.Info("CreatePotentialCustomer- Creating a new customer.");
            try
            {
                PotentialCustomer newCustomer = new PotentialCustomer()
                {
                    CustomerName = customer.CustomerName,
                    CustomerPhone = customer.CustomerPhone,
                    CustomerEmail = customer.CustomerEmail,
                    CustomerCity = customer.CustomerCity,
                    CustomerCountry = customer.CustomerCountry
                };

                _context.PotentialCustomers.Add(newCustomer);
                await _context.SaveChangesAsync();
                int currentCustomerId = newCustomer.CustomerId;

                logger.Info($"Created new customer with ID: {currentCustomerId}");

                return CreatedAtAction("GetCustomer", new { id = newCustomer.CustomerId }, newCustomer);
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while creating a new customer: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes a customer from the database.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>An ActionResult of PotentialCustomer.</returns>
        [HttpDelete("{id}")]
        [EnableCors("AllowOrigin")]
        [Authorize]
        public async Task<ActionResult<PotentialCustomer>> DeleteCustomers(int id)
        {
            try
            {
                var customer = await _context.PotentialCustomers.FindAsync(id);
                if (customer == null)
                {
                    logger.Warn($"Customer with ID {id} not found.");
                    return NotFound(new { Message = "Customer was not found" });
                }

                _context.PotentialCustomers.Remove(customer);
                await _context.SaveChangesAsync();

                logger.Info($"Deleted customer with ID: {id}");

                return Ok(new { Message = "Customer deleted successfully" });
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while deleting the customer with ID {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Checks if a customer exists in the database.
        /// </summary>
        /// <param name="id">The ID of the customer to check.</param>
        /// <returns>True if the customer exists, otherwise false.</returns>
        private bool CustomerExists(int id)
        {
            return _context.PotentialCustomers.Any(e => e.CustomerId == id);
        }
    }
}
