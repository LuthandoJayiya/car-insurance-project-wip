using System;
using System.Collections.Generic;

namespace LLJ_CarInsuranceMS_RESTAPI.Models
{
    public partial class PolicyApplication
    {
        public int ApplicationId { get; set; }
        public int CustomerId { get; set; }
        public int PolicyId { get; set; }
        public string? ApplicationStatus { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? RejectedReason { get; set; }

        public virtual PotentialCustomer Customer { get; set; } = null!;
        public virtual Policy Policy { get; set; } = null!;
    }
}
