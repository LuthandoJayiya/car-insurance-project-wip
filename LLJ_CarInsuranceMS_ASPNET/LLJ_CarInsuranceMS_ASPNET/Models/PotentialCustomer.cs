using System;
using System.Collections.Generic;

namespace LLJ_CarInsuranceMS_ASPNET.Models
{
    public partial class PotentialCustomer
    {
        public PotentialCustomer()
        {
            PolicyApplications = new HashSet<PolicyApplication>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? CustomerPhone { get; set; }
        public string CustomerEmail { get; set; } = null!;
        public string? CustomerCity { get; set; }
        public string? CustomerCountry { get; set; }
        public string? IdentintyUsername { get; set; }

        public virtual ICollection<PolicyApplication> PolicyApplications { get; set; }
    }
}
