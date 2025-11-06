using System;
using System.Collections.Generic;

namespace LLJ_CarInsuranceMS_RESTAPI.Models
{
    public partial class InsuranceClaim
    {
        public InsuranceClaim()
        {
            TransactionHistories = new HashSet<TransactionHistory>();
        }

        public int ClaimId { get; set; }
        public string AccidentDate { get; set; } = null!;
        public string ClaimStatus { get; set; } = null!;
        public string? ClaimName { get; set; }
        public int DriverId { get; set; }
        public int ClaimTypeId { get; set; }
        public int SurveyorId { get; set; }
        public int ShopId { get; set; }

        public virtual InsuranceClaimType ClaimType { get; set; } = null!;
        public virtual Driver Driver { get; set; } = null!;
        public virtual RepairShop Shop { get; set; } = null!;
        public virtual Surveyor Surveyor { get; set; } = null!;
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}
