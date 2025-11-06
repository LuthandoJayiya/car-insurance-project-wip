using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POCOS
{
    public class InsuranceClaim
    {
        [Key, Required, Column("claim_id")]
        public int ClaimID { get; set; }

        [Column("accident_date")]
        public string AccidentDate { get; set; }

        [Column("claim_status")]
        public string Status { get; set; }

        [Column("claim_name")]
        public string ClaimName { get; set; }

        [ForeignKey(nameof(Driver)), Required]
        [Column("driver_id")]
        public int DriverID { get; set; }

        [ForeignKey(nameof(ClaimType)),Column("claim_type_id")]
        public int ClaimTypeId { get; set; }

        [ForeignKey(nameof(Surveyor)), Required]
        [Column("surveyor_id")]
        public int SurveyorId { get; set; }

        [ForeignKey(nameof(RepairShop)), Required]
        [Column("shop_id")]
        public int ShopID { get; set; }


        //Navigation properties
        public virtual Driver Driver { get; set; }
        public virtual InsuranceClaimTypes ClaimType { get; set; }
        public virtual Surveyor Surveyor { get; set; }
        public virtual RepairShop RepairShop { get; set; }

        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }

    }
}
