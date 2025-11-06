using System;
using System.Collections.Generic;

namespace LLJ_CarInsuranceMS_ASPNET.Models
{
    public partial class TransactionHistory
    {
        public int TransactionId { get; set; }
        public string? TransactionName { get; set; }
        public string? TransactionDate { get; set; }
        public string? TransactionType { get; set; }
        public decimal TransactionAmount { get; set; }
        public int ClaimId { get; set; }
        public int ProcessorId { get; set; }

        public virtual InsuranceClaim Claim { get; set; } = null!;
        public virtual PaymentProcessor Processor { get; set; } = null!;
    }
}
