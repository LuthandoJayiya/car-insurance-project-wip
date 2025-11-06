namespace LLJ_CarInsuranceMS_RESTAPI.ViewModel
{
    /// <summary>
    /// ViewModel for PotentialCustomer objects, used for representing customer data in API responses.
    /// </summary>
    public class PotentialCustomerVM
    {
        /// <summary>
        /// Gets or sets the ID of the customer.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string CustomerName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the phone number of the customer.
        /// </summary>
        public string? CustomerPhone { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        public string CustomerEmail { get; set; } = null!;

        /// <summary>
        /// Gets or sets the city of the customer.
        /// </summary>
        public string? CustomerCity { get; set; }

        /// <summary>
        /// Gets or sets the country of the customer.
        /// </summary>
        public string? CustomerCountry { get; set; }

        /// <summary>
        /// Gets or sets the Identity Username of the customer.
        /// </summary>
        public string? IdentintyUsername { get; set; }

        /// <summary>
        /// Gets or sets the agent id to the customer.
        /// </summary>
        public int AgentID { get; set; }
    }
}
