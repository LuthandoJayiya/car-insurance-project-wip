using System;
using System.Collections.Generic;

namespace LLJ_CarInsuranceMS_RESTAPI.Models
{
    public partial class InsuranceClaimType
    {
        public InsuranceClaimType()
        {
            InsuranceClaims = new HashSet<InsuranceClaim>();
        }

        public int ClaimTypeId { get; set; }
        public string? ClaimTypeName { get; set; }
        public string? ClaimTypeDescription { get; set; }

        public virtual ICollection<InsuranceClaim> InsuranceClaims { get; set; }
    }
}
