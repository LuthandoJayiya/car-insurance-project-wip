using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCOS
{
    public class PaymentProcessor
    {
        [Key, Required, Column("processor_id")]
        public int ProcessorID { get; set; }

        [Column("processor_name")]
        public string ProcessorName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("payment_gateway")]
        public string PaymentGateway { get; set; }

        [Column("payment_amount")]
        public decimal ProcessorAmount { get; set; }

        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}
