using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCOS
{
    public class TransactionHistory
    {
        [Key, Required, Column("Transaction_id")]
        public int TransactionID { get; set; }

        [Column("Transaction_name")]
        public string TransactionName { get; set; }

        [Column("Transaction_date")]
        public string TransactionDate { get; set; }

        [Column("Transaction_type")]
        public string TransactionType { get; set; }

        [Column("Transaction_amount")]
        public decimal TransactionAmount { get; set; }

        [ForeignKey(nameof(Claim)), Required, Column("Claim_id")]
        public int ClaimID { get; set; }

        [ForeignKey(nameof(Processor)), Required, Column("Processor_id")]
        public int ProcessorID { get; set; }

        public virtual InsuranceClaim Claim { get; set; }

        public virtual PaymentProcessor Processor { get; set; }
    }
}
