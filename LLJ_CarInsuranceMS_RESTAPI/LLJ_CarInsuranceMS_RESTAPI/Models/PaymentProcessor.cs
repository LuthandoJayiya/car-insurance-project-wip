using System;
using System.Collections.Generic;

namespace LLJ_CarInsuranceMS_RESTAPI.Models
{
    public partial class PaymentProcessor
    {
        public PaymentProcessor()
        {
            TransactionHistories = new HashSet<TransactionHistory>();
        }

        public int ProcessorId { get; set; }
        public string? ProcessorName { get; set; }
        public string? Email { get; set; }
        public string? PaymentGateway { get; set; }
        public decimal PaymentAmount { get; set; }

        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}
