using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCOS
{
    public class PolicyApplication
    {
        [Key, Column("application_id")]
        public int ApplicationId { get; set; }

        [ForeignKey(nameof(Customer)), Column("customer_id")]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(Policy)), Column("policy_id")]
        public int PolicyId { get; set; }

        [Column("application_status")]
        public string Status { get; set; }

        [Column("submitted_date")]
        public DateTime? SubmittedDate { get; set; }

        [Column("reviewed_date")]
        public DateTime? ReviewedDate { get; set; }

        [Column("approved_date")]
        public DateTime? ApprovedDate { get; set; }

        [Column("rejected_reason")]
        public string RejectedReason { get; set; }

        public PotentialCustomer Customer { get; set; }
        public Policy Policy { get; set; }

    }
}
