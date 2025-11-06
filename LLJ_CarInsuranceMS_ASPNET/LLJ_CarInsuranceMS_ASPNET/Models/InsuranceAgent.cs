using System;
using System.Collections.Generic;

namespace LLJ_CarInsuranceMS_ASPNET.Models
{
    public partial class InsuranceAgent
    {
        public InsuranceAgent()
        {
            Policies = new HashSet<Policy>();
            PotentialCustomers = new HashSet<PotentialCustomer>();
        }

        public int AgentId { get; set; }
        public string? AgentFirstName { get; set; }
        public string? AgentLastName { get; set; }
        public string? AgentContactNumber { get; set; }
        public string? AgentEmail { get; set; }
        public string? Location { get; set; }
        public string? LicenseNumber { get; set; }
        public string? CommissionRate { get; set; }
        public string? CustomerFeedback { get; set; }

        public virtual ICollection<Policy> Policies { get; set; }
        public virtual ICollection<PotentialCustomer> PotentialCustomers { get; set; }
    }
}
