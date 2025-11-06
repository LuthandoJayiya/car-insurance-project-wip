using System;
using System.Collections.Generic;

namespace LLJ_CarInsuranceMS_ASPNET.Models
{
    public partial class Driver
    {
        public Driver()
        {
            InsuranceClaims = new HashSet<InsuranceClaim>();
        }

        public int DriverId { get; set; }
        public int AccidentsReported { get; set; }
        public string DriverFirstName { get; set; } = null!;
        public string? DriverLastName { get; set; }
        public string LicenseNumber { get; set; } = null!;
        public string? ContactInfo { get; set; }
        public string? RiskProfile { get; set; }
        public int PolicyId { get; set; }

        public virtual Policy Policy { get; set; } = null!;
        public virtual ICollection<InsuranceClaim> InsuranceClaims { get; set; }
    }
}
