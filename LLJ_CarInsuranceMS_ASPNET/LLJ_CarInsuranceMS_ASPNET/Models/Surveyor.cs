using System;
using System.Collections.Generic;

namespace LLJ_CarInsuranceMS_ASPNET.Models
{
    public partial class Surveyor
    {
        public Surveyor()
        {
            InsuranceClaims = new HashSet<InsuranceClaim>();
        }

        public int SurveyorId { get; set; }
        public string? Name { get; set; }
        public string? LicenseNumber { get; set; }
        public string? ContactInfo { get; set; }

        public virtual ICollection<InsuranceClaim> InsuranceClaims { get; set; }
    }
}
