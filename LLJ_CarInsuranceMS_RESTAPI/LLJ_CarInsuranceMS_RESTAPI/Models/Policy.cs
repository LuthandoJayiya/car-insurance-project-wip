using System;
using System.Collections.Generic;

namespace LLJ_CarInsuranceMS_RESTAPI.Models
{
    public partial class Policy
    {
        public Policy()
        {
            Drivers = new HashSet<Driver>();
            PolicyApplications = new HashSet<PolicyApplication>();
            Vehicles = new HashSet<Vehicle>();
        }

        public int PolicyId { get; set; }
        public string? PolicyType { get; set; }
        public string? PolicyName { get; set; }
        public string? Coverage { get; set; }
        public double Premium { get; set; }
        public string? ExpirationDate { get; set; }
        public string? CreationDate { get; set; }

        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<PolicyApplication> PolicyApplications { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
